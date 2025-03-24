using System.Collections.Generic;

public class FakeInventoryPersistence : IInventoryPersistence
{
    public List<Pog> SavedPogs = new List<Pog>();

    public void SaveInventory(List<Pog> pogs)
    {
        SavedPogs = new List<Pog>(pogs);
    }

    public List<Pog> LoadInventory()
    {
        return SavedPogs;
    }
}
