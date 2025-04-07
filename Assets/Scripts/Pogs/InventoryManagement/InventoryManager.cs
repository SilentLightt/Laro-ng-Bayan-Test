using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Pog> ownedPogs = new List<Pog>();
    private List<Pog> matchLoadout = new List<Pog>();
    private const int maxLoadout = 10;

    // Dependencies injected via Initialize or defaulted in Awake.
    private IInventoryPersistence persistence;
    private IUpgradeService upgradeService;
    private IStarterPackProvider starterPackProvider;
    private InventoryUI inventoryUI;
    public List<Pog> GetOwnedPogs()
    {
        return ownedPogs;
    }
    public void OpenStarterPack()
    {
        if (!starterPackProvider.HasStarterPackOpened())
        {
            List<Pog> starterPogs = starterPackProvider.GetStarterPack();
            ownedPogs.AddRange(starterPogs);
            starterPackProvider.MarkStarterPackOpened();
            persistence.SaveInventory(ownedPogs);
            Debug.Log("Starter pack opened!");
        }
    }

    public void Initialize(IInventoryPersistence persistence, IUpgradeService upgradeService, IStarterPackProvider starterPackProvider)
    {
        this.persistence = persistence;
        this.upgradeService = upgradeService;
        this.starterPackProvider = starterPackProvider;
    }

    private void Awake()
    {
        // Provide default implementations if none are injected.
        if (persistence == null) persistence = new JSONInventoryPersistence();
        if (upgradeService == null) upgradeService = new PogUpgradeService();
        if (starterPackProvider == null) starterPackProvider = new StarterPackProvider();

        // Load existing inventory.
        ownedPogs = persistence.LoadInventory();

        // Award starter pack if not yet opened.
        if (!starterPackProvider.HasStarterPackOpened())
        {
            List<Pog> starterPogs = starterPackProvider.GetStarterPack();
            ownedPogs.AddRange(starterPogs);
            starterPackProvider.MarkStarterPackOpened();
            persistence.SaveInventory(ownedPogs);
            Debug.Log("Starter pack granted: " + starterPogs.Count + " pogs added!");
        }
    }

    public void AddPog(Pog newPog)
    {
        // Find an existing pog of the same type.
        Pog existingPog = ownedPogs.Find(p => p.id == newPog.id);
        if (existingPog != null)
        {
            existingPog.duplicateCount++;
            upgradeService.ProcessUpgrade(existingPog);
        }
        else
        {
            ownedPogs.Add(newPog);
        }
        persistence.SaveInventory(ownedPogs);
    }

    public void RemovePog(string pogId)
    {
        Pog existingPog = ownedPogs.Find(p => p.id == pogId);
        if (existingPog != null)
        {
            ownedPogs.Remove(existingPog);
            persistence.SaveInventory(ownedPogs);
        }
    }

    public bool SelectPogForMatch(Pog pog)
    {
        if (matchLoadout.Count >= maxLoadout)
        {
            Debug.Log("Maximum loadout reached.");
            return false;
        }
        matchLoadout.Add(pog);
        return true;
    }

    public void DeselectPogFromMatch(Pog pog)
    {
        if (matchLoadout.Contains(pog))
            matchLoadout.Remove(pog);
    }
}
