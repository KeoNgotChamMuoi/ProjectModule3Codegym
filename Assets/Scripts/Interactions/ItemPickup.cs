using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Reference to the item being picked up

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item); // Add the item to the playerState's inventory
                Interact(); // Handle the interaction (e.g., destroy the item)
            }
        }
    }
    public void Interact()
    {
        // Logic for picking up the item (e.g., add to inventory, apply effects)
        Debug.Log("Pickup " + item.itemName);
        Destroy(gameObject); // Remove the item from the scene
    }
}
