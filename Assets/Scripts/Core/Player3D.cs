using Game.Core;
using Game.Interfaces;
using UnityEngine;

public class Player3D : BaseEntity
{
    public static Player3D Instance { get; private set; }
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    [Header("Rotation")]
    public float rotationSpeed = 10f;

    [Header("Movement Strategy")]
    public IMovementStrategy moveStrategy;

    //[Header("Animation")]
    [SerializeField] private Animator animator;

    //[Header("Respawn")]
    //[SerializeField] private Transform respawnPoint;
    //[SerializeField] private float respawnDelay = 3f;


    InputManager input;
    private bool deathStarted = false;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        input = InputManager.Instance;
    }

    public void CheckInteract()
    {
        // Interaction logic here (e.g., raycast for objects)
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        // Additional playerState-specific damage logic (e.g., update UI, play sound)
    }
    public override void Die()
    {
        if (deathStarted) return; // Prevent multiple death triggers
        deathStarted = true;
        isDead = true;
        moveStrategy = null; // Disable movement

        // Additional playerState-specific death logic (e.g., play animation, disable controls)
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        if (InputManager.Instance != null)
            InputManager.Instance.enabled = false;
        // Disable playerState controls here
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
    }

    public void Move()
    {

    }
    public void OnDeathAnimationEnd()
    {
        Debug.Log("Player death animation ended. Game Over logic can be triggered here.");
        Destroy(gameObject); // Example: Destroy playerState object after death animation
    }
    //IEnumerator RespawnCoroutine()
    //{
    //    yield return new WaitForSeconds(respawnDelay); // Wait for 3 seconds before respawning
    //    Respawm();
    //}

    //void Respawm()
    //{
    //    Debug.Log("Player respawned.");
    //    isDead = false;
    //    deathStarted = false;
    //    // Reset playerState position, health, and other necessary states here
    //    transform.position = Vector3.zero; // Example: Respawn at origin
    //    health = 100; // Reset health
    //                  // Re-enable playerState controls here
    //}
}