using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("PlayerMovement đang chạy trên: " + gameObject.name);
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }
}