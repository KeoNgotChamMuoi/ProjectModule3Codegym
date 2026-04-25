using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public Inventory inventory;
    public Item[] testItem;

    void Start()
    {
        Item[] items = Resources.LoadAll<Item>("Items");

        foreach (Item item in items)
        {
            inventory.AddItem(item);
        }
    }
}