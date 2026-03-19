using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxInventorySize = 20;
    public void AddItem(Item item)
    {
        if (items.Count < maxInventorySize)
        {
            items.Add(item);
            Debug.Log("Added item: " + item.itemName);
        }
        else
        {
            Debug.Log("Inventory is full! Cannot add item: " + item.itemName);
        }
    }
}
