using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("References")]
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerAnimation playerAnimation;
    public PlayerDeath playerDeath;

    [Header("Components")]
    public Rigidbody2D playerRigidbody;
    public Animator playerAnimator;

    protected override void Awake()
    {
        base.Awake();
        
        playerInput = GetComponentInChildren<PlayerInput>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerDeath = GetComponentInChildren<PlayerDeath>();

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    public void TelePlayer(Vector3 pos)
    {
        transform.position = pos;
    }
}
