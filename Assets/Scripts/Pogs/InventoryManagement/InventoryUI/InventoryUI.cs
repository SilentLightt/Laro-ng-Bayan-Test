using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject pogSlotPrefab;
    [SerializeField] private GameObject pogDetailsPanel;
    [SerializeField] private Text pogNameText;
    [SerializeField] private Text pogPowerText;
    [SerializeField] private Text pogRarityText;
    [SerializeField] private Text upgradeProgressText;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject openStarterPackPanel;
    [SerializeField] private Button openPackButton;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        closeButton.onClick.AddListener(() => pogDetailsPanel.SetActive(false));
        openPackButton.onClick.AddListener(OpenStarterPack);

        LoadInventory();
    }

    private void LoadInventory()
    {
        ClearInventoryUI();
        List<Pog> pogs = inventoryManager.GetOwnedPogs();

        foreach (var pog in pogs)
        {
            GameObject slot = Instantiate(pogSlotPrefab, gridParent);
            slot.GetComponent<PogSlot>().Initialize(pog, this);
        }

        // Check if the starter pack was already opened
        if (PlayerPrefs.GetInt("StarterPackOpened", 0) == 0)
        {
            openStarterPackPanel.SetActive(true);
        }
        else
        {
            openStarterPackPanel.SetActive(false);
        }
    }

    public void ShowPogDetails(Pog pog)
    {
        pogDetailsPanel.SetActive(true);
        pogNameText.text = pog.id;
        pogPowerText.text = $"Power: {pog.CurrentPower}";
        pogRarityText.text = $"Rarity: {pog.rarity}";
        upgradeProgressText.text = $"Upgrades: {pog.duplicateCount}/{pog.UpgradeThreshold()}";
    }

    private void OpenStarterPack()
    {
        inventoryManager.OpenStarterPack();
        PlayerPrefs.SetInt("StarterPackOpened", 1);
        openStarterPackPanel.SetActive(false);
        LoadInventory(); // Refresh UI
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }
}
