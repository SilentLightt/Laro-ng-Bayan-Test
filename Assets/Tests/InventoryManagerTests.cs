using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class InventoryManagerTests
{
    private InventoryManager inventoryManager;
    private FakeInventoryPersistence fakePersistence;
    private IUpgradeService upgradeService;
    private IStarterPackProvider starterPackProvider;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for the InventoryManager component.
        GameObject inventoryGO = new GameObject("InventoryManager");
        inventoryManager = inventoryGO.AddComponent<InventoryManager>();

        // Create fake dependencies.
        fakePersistence = new FakeInventoryPersistence();
        upgradeService = new PogUpgradeService();
        starterPackProvider = new StarterPackProvider();

        // Inject dependencies.
        inventoryManager.Initialize(fakePersistence, upgradeService, starterPackProvider);

        // For testing, we might want to simulate that the starter pack is already opened.
        PlayerPrefs.SetInt("StarterPackOpened", 1);
    }

    [Test]
    public void AddPog_AddsNewPogIfNotExisting()
    {
        // Arrange
        Pog newPog = new Pog("TestPog", PogRarity.Common, 10);

        // Act
        inventoryManager.AddPog(newPog);

        // Assert: Fake persistence should now have the new pog.
        List<Pog> savedPogs = fakePersistence.LoadInventory();
        Assert.AreEqual(1, savedPogs.Count);
        Assert.AreEqual("TestPog", savedPogs[0].id);
    }

    [Test]
    public void AddPog_UpgradesExistingPogWhenDuplicateAdded()
    {
        // Arrange: Add a pog.
        Pog newPog = new Pog("TestPog", PogRarity.Common, 10);
        inventoryManager.AddPog(newPog);

        // Act: Add a duplicate so that duplicateCount reaches threshold.
        for (int i = 0; i < 10; i++)
        {
            inventoryManager.AddPog(new Pog("TestPog", PogRarity.Common, 10));
        }

        // Retrieve the pog from fake persistence.
        List<Pog> savedPogs = fakePersistence.LoadInventory();
        Pog existingPog = savedPogs.Find(p => p.id == "TestPog");

        // Assert: Should have upgraded to level 2 (assuming threshold of 10 for Common).
        Assert.IsNotNull(existingPog);
        Assert.AreEqual(2, existingPog.level);
    }
}
