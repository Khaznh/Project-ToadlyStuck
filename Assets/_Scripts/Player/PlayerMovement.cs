using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("LV23 stats")]
    public bool isHighPing = false;
    [SerializeField] private float highPingDelay = 1.5f;
    private Queue<DelayInput> delayInputs = new Queue<DelayInput>();
    private Vector2 currentDelayedMoveVector = Vector2.zero;

    [Header("LV24 stats")]
    public bool isHopHop = false;

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
        if (isHighPing)
        {
            delayInputs.Enqueue(new DelayInput { moveVector = input.moveVector, delay = Time.time + highPingDelay });

            while (delayInputs.Count > 0 && delayInputs.Peek().delay <= Time.time)
            {
                currentDelayedMoveVector = delayInputs.Dequeue().moveVector;
            }

            HandleMovement(currentDelayedMoveVector);
            return;
        }

        HandleMovement(input.moveVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;

            if (isHopHop)
            {
                HandleJump();
            }
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
        }
        else
        {
            rb.linearVelocity = new Vector2(moveVector.x * moveSpeed - windForce, rb.linearVelocity.y);
        }
    }

    private void HandleFacing()
    {
        Vector3 currentScale = PlayerController.Instance.transform.localScale;

        if (isHighPing)
        {
            if (currentDelayedMoveVector.x > 0)
            {
                PlayerController.Instance.transform.localScale = new Vector3(1, PlayerController.Instance.transform.localScale.y, PlayerController.Instance.transform.localScale.z);
            }
            else if (currentDelayedMoveVector.x < 0)
            {
                PlayerController.Instance.transform.localScale = new Vector3(-1, PlayerController.Instance.transform.localScale.y, PlayerController.Instance.transform.localScale.z);
            }

            return;
        }

        if (input.moveVector.x > 0)
        {
            PlayerController.Instance.transform.localScale = new Vector3(1, PlayerController.Instance.transform.localScale.y, PlayerController.Instance.transform.localScale.z);
        }
        else if (input.moveVector.x < 0)
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

        if (isHighPing)
        {
            StartCoroutine(DelayedJumpCoroutine());
            return;
        }

        ExecuteJump();
    }

    private void ExecuteJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isTouchingGround = false;
        OnPlayerJump?.Invoke();
    }

    private IEnumerator DelayedJumpCoroutine()
    {
        yield return new WaitForSeconds(highPingDelay);
        ExecuteJump();
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

        delayInputs.Clear();
        currentDelayedMoveVector = Vector2.zero;
    }
}

public struct DelayInput
{
    public Vector2 moveVector;
    public float delay;
}
