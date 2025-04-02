using System.Collections.Generic;
using UnityEngine;

public class StarterPackProvider : IStarterPackProvider
{
    private const string StarterPackKey = "StarterPackOpened";

    private static readonly Dictionary<PogRarity, (int count, float basePower)> starterPackDistribution =
        new Dictionary<PogRarity, (int, float)>
        {
            { PogRarity.Common, (8, 10) },
            { PogRarity.Uncommon, (6, 12) },
            { PogRarity.Rare, (4, 15) },
            { PogRarity.Epic, (2, 20) }
        };

    public List<Pog> GetStarterPack()
    {
        List<Pog> pack = new List<Pog>();

        foreach (var entry in starterPackDistribution)
        {
            PogRarity rarity = entry.Key;
            int count = entry.Value.count;
            float basePower = entry.Value.basePower;

            for (int i = 0; i < count; i++)
            {
                string pogId = $"Starter_{rarity}_{i}";
                pack.Add(new Pog(pogId, rarity, basePower));
            }
        }

        return pack;
    }

    public bool HasStarterPackOpened()
    {
        return PlayerPrefs.GetInt(StarterPackKey, 0) == 1;
    }

    public void MarkStarterPackOpened()
    {
        PlayerPrefs.SetInt(StarterPackKey, 1);
        PlayerPrefs.Save();
    }
}

//using System.Collections.Generic;
//using UnityEngine;

//public class StarterPackProvider : IStarterPackProvider
//{
//    private const string StarterPackKey = "StarterPackOpened";

//    public List<Pog> GetStarterPack()
//    {
//        List<Pog> pack = new List<Pog>();
//        // Example distribution: 8 Common, 6 Uncommon, 4 Rare, 2 Epic
//        for (int i = 0; i < 8; i++)
//            pack.Add(new Pog("Common_" + i, PogRarity.Common, 10));
//        for (int i = 0; i < 6; i++)
//            pack.Add(new Pog("Uncommon_" + i, PogRarity.Uncommon, 12));
//        for (int i = 0; i < 4; i++)
//            pack.Add(new Pog("Rare_" + i, PogRarity.Rare, 15));
//        for (int i = 0; i < 2; i++)
//            pack.Add(new Pog("Epic_" + i, PogRarity.Epic, 20));
//        return pack;
//    }

//    public bool HasStarterPackOpened()
//    {
//        return PlayerPrefs.GetInt(StarterPackKey, 0) == 1;
//    }

//    public void MarkStarterPackOpened()
//    {
//        PlayerPrefs.SetInt(StarterPackKey, 1);
//        PlayerPrefs.Save();
//    }
//}
