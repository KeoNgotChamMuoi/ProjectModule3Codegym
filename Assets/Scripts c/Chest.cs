using UnityEngine;

public class Chest : MonoBehaviour
{
    [System.Serializable]
    public class ItemStack
    {
        public Item item;     
        public int amount;
    }

    public ItemStack[] items;        
    public InventorySlot[] slots;   
    public Inventory inventory;

    private bool playerInRange = false;
    private bool opened = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        Debug.Log("OPEN CHEST CALLED");
        opened = true;
        Debug.Log("Chest opened!");

     
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Length && items[i].item != null)
            {
                slots[i].SetItem(items[i].item, items[i].amount);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}