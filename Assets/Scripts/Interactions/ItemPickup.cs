using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic for picking up the item (e.g., add to inventory, apply effects)
            Debug.Log("Item picked up!");
            Destroy(gameObject); // Remove the item from the scene
        }
    }
}
