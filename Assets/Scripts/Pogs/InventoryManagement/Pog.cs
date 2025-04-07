using System;
using System.Collections.Generic;
using UnityEngine;

public enum PogRarity { Common, Uncommon, Rare, Epic }
[CreateAssetMenu(fileName = "New Pog", menuName = "Pogs/Pog")]

[Serializable]
public class Pog : ScriptableObject
{
    public string id;
    public PogRarity rarity;
    public int level;
    public int duplicateCount;
    public float basePower;

    private const int MaxLevel = 5;

    private static readonly Dictionary<PogRarity, int> PowerBonusPerLevel = new Dictionary<PogRarity, int>
    {
        { PogRarity.Common, 2 },
        { PogRarity.Uncommon, 3 },
        { PogRarity.Rare, 4 },
        { PogRarity.Epic, 5 }
    };

    private static readonly Dictionary<PogRarity, int> UpgradeThresholds = new Dictionary<PogRarity, int>
    {
        { PogRarity.Common, 10 },
        { PogRarity.Uncommon, 12 },
        { PogRarity.Rare, 6 },
        { PogRarity.Epic, 3 }
    };

    /// <summary>
    /// Calculates the current power of the Pog, factoring in its level and rarity.
    /// </summary>
    public float CurrentPower => basePower + PowerBonusPerLevel[rarity] * (level - 1);

    public Pog(string id, PogRarity rarity, float basePower)
    {
        this.id = id;
        this.rarity = rarity;
        this.basePower = basePower;
        this.level = 1; // Ensures level starts at 1
        this.duplicateCount = 0;
    }

    /// <summary>
    /// Returns the required duplicate count for the Pog to level up.
    /// </summary>
    public int UpgradeThreshold()
    {
        return UpgradeThresholds.TryGetValue(rarity, out int threshold) ? threshold : int.MaxValue;
    }
}


//pinaka base script neto
//using System;
//using UnityEngine;

//public enum PogRarity { Common, Uncommon, Rare, Epic }

//[Serializable]
//public class Pog
//{
//    public string id;
//    public PogRarity rarity;
//    public int level;
//    public int duplicateCount;
//    public float basePower;

//    public float CurrentPower
//    {
//        get
//        {
//            int bonus = 0;
//            switch (rarity)
//            {
//                case PogRarity.Common: bonus = 2 * (level - 1); break;
//                case PogRarity.Uncommon: bonus = 3 * (level - 1); break;
//                case PogRarity.Rare: bonus = 4 * (level - 1); break;
//                case PogRarity.Epic: bonus = 5 * (level - 1); break;
//            }
//            return basePower + bonus;
//        }
//    }

//    public Pog(string id, PogRarity rarity, float basePower)
//    {
//        this.id = id;
//        this.rarity = rarity;
//        this.basePower = basePower;
//        this.level = 1;
//        this.duplicateCount = 0;
//    }

//    public int UpgradeThreshold()
//    {
//        switch (rarity)
//        {
//            case PogRarity.Common: return 10;
//            case PogRarity.Uncommon: return 12;
//            case PogRarity.Rare: return 6;
//            case PogRarity.Epic: return 3;
//            default: return int.MaxValue;
//        }
//    }
//}
