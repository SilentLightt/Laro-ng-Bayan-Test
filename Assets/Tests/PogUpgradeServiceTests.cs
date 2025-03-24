using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PogUpgradeServiceTests
{
    [Test]
    public void ProcessUpgrade_UpgradesWhenThresholdReached()
    {
        // Arrange: Create a Common pog with a threshold of 10 duplicates.
        Pog pog = new Pog("TestPog", PogRarity.Common, basePower: 10);
        pog.duplicateCount = 10; // Exactly the threshold.
        IUpgradeService upgradeService = new PogUpgradeService();

        // Act: Process the upgrade.
        upgradeService.ProcessUpgrade(pog);

        // Assert: Level should increase from 1 to 2 and duplicateCount reset.
        Assert.AreEqual(2, pog.level);
        Assert.AreEqual(0, pog.duplicateCount);
    }

    [Test]
    public void ProcessUpgrade_DoesNotUpgradeBeyondMaxLevel()
    {
        // Arrange: Create a Rare pog at level 5 (max).
        Pog pog = new Pog("RareTestPog", PogRarity.Rare, basePower: 15)
        {
            level = 5,
            duplicateCount = 10  // Exceeding the threshold.
        };
        IUpgradeService upgradeService = new PogUpgradeService();

        // Act: Process upgrade.
        upgradeService.ProcessUpgrade(pog);

        // Assert: Level should remain at 5 and duplicate count remains unchanged.
        Assert.AreEqual(5, pog.level);
        Assert.AreEqual(10, pog.duplicateCount);
    }
}
