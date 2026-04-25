using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;

    public void AddItem(Item item, int amount = 1)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item, amount);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }
}