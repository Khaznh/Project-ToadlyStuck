using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isTouchingGround = true;

    private PlayerInput input;
    private Rigidbody2D rb;

    private void Start()
    {
        input = PlayerController.Instance.playerInput;
        rb = PlayerController.Instance.playerRigidbody;

        input.OnJumpClicked += HandleJump;
    }

    private void Update()
    {
        HandleFacing();
    }

    private void FixedUpdate()
    {
        HandleMovement(input.moveVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }

    private void HandleMovement(Vector2 moveVector)
    {
        rb.linearVelocity = new Vector2(moveVector.x * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFacing()
    {
        if (input.moveVector.x > 0)
        {
            PlayerController.Instance.transform.localScale = new Vector3(1, 1, 1);
        } else if (input.moveVector.x < 0)
        {
            PlayerController.Instance.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        if (!isTouchingGround) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isTouchingGround = false;
    }
}
