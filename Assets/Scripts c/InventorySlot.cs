using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; 


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler

{
    public enum SlotType
    {
        Inventory,
        Chest,
    }
    public SlotType slotType;

    public Image icon;
    public TextMeshProUGUI amountText;

    public Item item;
    public int amount;

    private Transform originalParent;
    private Vector3 originalPosition;


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
    

    public void Clear()
    {
        item = null;
        amount = 0;

        icon.sprite = null;
        icon.enabled = false;
        icon.color = new Color(1f, 1f, 1f, 0f);
        amountText.text = "";
    }

    public void SetItem(Item newItem, int newAmount)
    {
        Debug.Log("Item: " + newItem);
        Debug.Log("Icon: " + (newItem != null ? newItem.icon : null));
        if (newItem == null)
        {
            Debug.LogError("Item is NULL!");
            return;
        }
        if (newItem.icon == null)
        {
            Debug.LogWarning("Item has no icon!");
            icon.enabled = true;
            icon.color = Color.red;
            return;
        }

        item = newItem;
        amount = newAmount;

        icon.sprite = item.icon;
        icon.enabled = true;
        icon.color = Color.white; 

        amountText.text = amount.ToString();

        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseOutBack();
    }

    public bool IsEmpty()
    {
        return item == null;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty()) return;

        originalParent = icon.transform.parent;
        originalPosition = icon.transform.position;

        icon.raycastTarget = false;

       
        icon.transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsEmpty()) return;

        icon.transform.position = Input.mousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot otherSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
        if (otherSlot != null && otherSlot != this)
        {
            Item tempItem = item;
            int tempAmount = amount;
            SetItem(otherSlot.item, otherSlot.amount);
            otherSlot.SetItem(tempItem, tempAmount);

            if (tempItem == null)
                otherSlot.Clear();
            else
                otherSlot.SetItem(tempItem, tempAmount);

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.raycastTarget = true;

       
        icon.transform.SetParent(transform);
        icon.transform.localPosition = Vector3.zero;

        
        if (eventData.pointerEnter == null)
        {
            Debug.Log("Drop ra ngoài → vứt item");
            Clear();
        }
    }
}