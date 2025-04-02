public interface IInventoryPersistence
{
    void SaveInventory(System.Collections.Generic.List<Pog> pogs);
    System.Collections.Generic.List<Pog> LoadInventory();
}
