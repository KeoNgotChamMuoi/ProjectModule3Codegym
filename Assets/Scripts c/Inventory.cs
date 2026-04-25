using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;

    void Start()
    {
        // Tự lấy tất cả slot trong InventoryGrid
        slots = GetComponentsInChildren<InventorySlot>();
    }

    public void AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot == null) continue;

            if (slot.IsEmpty()) 
            {
                slot.SetItem(item, 1);
                return;
            }
        }

        Debug.Log("Inventory full!");
    }
}