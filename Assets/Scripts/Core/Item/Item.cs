using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
    Armor,
    Tool,
    Material,
    QuestItem
}
public class Item : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public string itemDescription;
    public int maxStackSize;
    public float itemWeight;
}
