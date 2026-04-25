using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    public GameObject chestUI;
    public PlayerMovement playerMovement;
    public Transform player;

    private bool isOpen = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < 3f )
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Đã nhấn E");
                ToggleChest();
            }  
                
        }
    }

    void ToggleChest()
    {
        isOpen = !isOpen;

        chestUI.SetActive(isOpen);

        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;

        playerMovement.canMove = !isOpen;
    }
}