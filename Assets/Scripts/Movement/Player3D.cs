using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class Player3D : BaseEntity
{
    [Header("Movement Strategy")]
    public IMovementStrategy moveStrategy;
    public Camera mainCamera;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 3f;


    InputManager input;
    private bool deathStarted = false;


    public void HandleInput()
    {
        Vector3 moveDirection = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0; // Keep movement horizontal
        moveStrategy.Move(transform, moveDirection, moveSpeed);
        if (input.JumpPressed)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                moveStrategy.Jump(rb, 5f); // Example jump force
            }
        }
    }
    public void CheckInteract()
    {
        // Interaction logic here (e.g., raycast for objects)
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        // Additional player-specific damage logic (e.g., update UI, play sound)
    }
    public override void Die()
    {
        if (deathStarted) return; // Prevent multiple death triggers
        deathStarted = true;
        isDead = true;
        moveStrategy = null; // Disable movement

        // Additional player-specific death logic (e.g., play animation, disable controls)
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        // Disable player controls here
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        HandleInput();
    }

    public void OnDeathAnimationEnd()
    {
        Debug.Log("Player death animation ended. Game Over logic can be triggered here.");
        Destroy(gameObject); // Example: Destroy player object after death animation
    }
    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay); // Wait for 3 seconds before respawning
        Respawm();
    }

    void Respawm()
    {
        Debug.Log("Player respawned.");
        isDead = false;
        deathStarted = false;
        // Reset player position, health, and other necessary states here
        transform.position = Vector3.zero; // Example: Respawn at origin
        health = 100; // Reset health
                      // Re-enable player controls here
    }
}
