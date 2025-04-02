using System.Collections.Generic;
using UnityEngine;

public class JSONInventoryPersistence : IInventoryPersistence
{
    private const string InventoryKey = "InventoryData";

    [System.Serializable]
    private class InventoryData
    {
        public List<Pog> pogs;
        public InventoryData(List<Pog> pogs)
        {
            this.pogs = pogs;
        }
    }

    public void SaveInventory(List<Pog> pogs)
    {
        InventoryData data = new InventoryData(pogs);
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(InventoryKey, jsonData);
        PlayerPrefs.Save();
    }

    public List<Pog> LoadInventory()
    {
        if (PlayerPrefs.HasKey(InventoryKey))
        {
            string jsonData = PlayerPrefs.GetString(InventoryKey);
            InventoryData data = JsonUtility.FromJson<InventoryData>(jsonData);
            return data.pogs;
        }
        return new List<Pog>();
    }
}
