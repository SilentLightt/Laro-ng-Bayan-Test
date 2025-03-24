using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class StarterPackProviderTests
{
    [Test]
    public void GetStarterPack_Returns20PogsWithCorrectDistribution()
    {
        // Arrange
        IStarterPackProvider starterPackProvider = new StarterPackProvider();

        // Act
        List<Pog> starterPack = starterPackProvider.GetStarterPack();

        // Assert: Should have 20 pogs total.
        Assert.AreEqual(20, starterPack.Count);

        // Distribution verification:
        int commonCount = starterPack.FindAll(p => p.rarity == PogRarity.Common).Count;
        int uncommonCount = starterPack.FindAll(p => p.rarity == PogRarity.Uncommon).Count;
        int rareCount = starterPack.FindAll(p => p.rarity == PogRarity.Rare).Count;
        int epicCount = starterPack.FindAll(p => p.rarity == PogRarity.Epic).Count;

        Assert.AreEqual(8, commonCount);
        Assert.AreEqual(6, uncommonCount);
        Assert.AreEqual(4, rareCount);
        Assert.AreEqual(2, epicCount);
    }
}
