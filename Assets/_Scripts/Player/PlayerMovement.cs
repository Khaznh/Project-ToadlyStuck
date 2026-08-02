using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action OnPlayerJump;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isTouchingGround = true;

    [Header("LV11 stats")]
    public bool isInWind = false;
    [SerializeField] private float windForce = 2.0f;

    [Header("LV15 stats")]
    public bool isBlockJump = false;

    [Header("LV17 stats")]
    public bool isReverseByGravity = false;

    [Header("LV21 stats")]
    public bool curseGravity = false;

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

        if (curseGravity)
        {
            if (!isTouchingGround)
            {
                return;
            }

            ReverseByGravity();
            return;
        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = false;
        }
    }

    private void HandleMovement(Vector2 moveVector)
    {
        if (!isInWind)
        {
            rb.linearVelocity = new Vector2(moveVector.x * moveSpeed, rb.linearVelocity.y);
        } else
        {
            rb.linearVelocity = new Vector2(moveVector.x * moveSpeed - windForce, rb.linearVelocity.y);
        }
    }

    private void HandleFacing()
    {
        Vector3 currentScale = PlayerController.Instance.transform.localScale;

        if (input.moveVector.x > 0)
        {    
            PlayerController.Instance.transform.localScale = new Vector3(1, PlayerController.Instance.transform.localScale.y, PlayerController.Instance.transform.localScale.z);
        } else if (input.moveVector.x < 0)
        {
            PlayerController.Instance.transform.localScale = new Vector3(-1, PlayerController.Instance.transform.localScale.y, PlayerController.Instance.transform.localScale.z);
        }
    }

    private void HandleJump()
    {
        if (curseGravity)
        {
            return;
        }

        if (isReverseByGravity)
        {
            ReverseByGravity();
            return;
        }

        if (!isTouchingGround) return;

        if (isBlockJump)
        {
            return;
        }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isTouchingGround = false;
        OnPlayerJump?.Invoke();
    }

    private void ReverseByGravity()
    {
        Vector3 currentScale = PlayerController.Instance.transform.localScale;

        PlayerController.Instance.transform.localScale = new Vector3(currentScale.x, -currentScale.y, currentScale.z);
        PlayerController.Instance.SetPlayerGravityScale(-PlayerController.Instance.playerRigidbody.gravityScale);
    }

    public void ResetForNextLevel()
    {
        PlayerController.Instance.transform.localScale = Vector3.one;
        PlayerController.Instance.SetPlayerGravityScale(Math.Abs(PlayerController.Instance.playerRigidbody.gravityScale));
    }
}
