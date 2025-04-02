using UnityEngine;

public class PogUpgradeService : IUpgradeService
{
    private const int MaxPogLevel = 5;

    public void ProcessUpgrade(Pog pog)
    {
        if (pog.level >= MaxPogLevel)
        {
            Debug.Log($"Pog {pog.id} is already at max level.");
            return;
        }

        int threshold = pog.UpgradeThreshold();
        bool upgraded = false;

        while (pog.duplicateCount >= threshold && pog.level < MaxPogLevel)
        {
            pog.duplicateCount -= threshold;
            pog.level++;
            upgraded = true;
        }

        if (upgraded)
        {
            Debug.Log($"Pog {pog.id} upgraded to level {pog.level}!");
        }
    }
}

//using UnityEngine;

//public class PogUpgradeService : IUpgradeService
//{
//    public void ProcessUpgrade(Pog pog)
//    {
//        int threshold = pog.UpgradeThreshold();
//        while (pog.duplicateCount >= threshold && pog.level < 5)
//        {
//            pog.duplicateCount -= threshold;
//            pog.level++;
//            Debug.Log($"Pog {pog.id} upgraded to level {pog.level}!");
//        }
//    }
//}
