using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class PogSlot : MonoBehaviour
{
    [SerializeField] private Image pogIcon;
    [SerializeField]  Text pogPower;
    [SerializeField]  Text pogLevel;
    private Pog pogData;
    private InventoryUI inventoryUI;

    public void Initialize(Pog pog, InventoryUI ui)
    {
        pogData = pog;
        inventoryUI = ui;

        // Set UI values
        pogPower.text = $"Power: {pog.CurrentPower}";
        pogLevel.text = $"Lvl: {pog.level}";

        // Assign rarity-based color
        pogIcon.color = GetRarityColor(pog.rarity);

        // Add button click listener
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        inventoryUI.ShowPogDetails(pogData);
    }

    private Color GetRarityColor(PogRarity rarity)
    {
        return rarity switch
        {
            PogRarity.Common => Color.white,
            PogRarity.Uncommon => Color.green,
            PogRarity.Rare => Color.blue,
            PogRarity.Epic => Color.magenta,
            _ => Color.gray,
        };
    }
}
