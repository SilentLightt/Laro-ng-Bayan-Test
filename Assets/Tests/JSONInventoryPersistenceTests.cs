using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class JSONInventoryPersistenceTests
{
    private JSONInventoryPersistence persistence;
    private const string TestInventoryKey = "InventoryData";

    [SetUp]
    public void Setup()
    {
        persistence = new JSONInventoryPersistence();
        PlayerPrefs.DeleteKey(TestInventoryKey);
    }

    [Test]
    public void SaveAndLoadInventory_ReturnsSameData()
    {
        // Arrange: Create test pogs.
        List<Pog> testPogs = new List<Pog>
        {
            new Pog("Pog1", PogRarity.Common, 10),
            new Pog("Pog2", PogRarity.Epic, 20)
        };

        // Act: Save and then load the inventory.
        persistence.SaveInventory(testPogs);
        List<Pog> loadedPogs = persistence.LoadInventory();

        // Assert: Check if the loaded list has the same count and data.
        Assert.AreEqual(testPogs.Count, loadedPogs.Count);
        Assert.AreEqual(testPogs[0].id, loadedPogs[0].id);
        Assert.AreEqual(testPogs[1].rarity, loadedPogs[1].rarity);
    }
}
