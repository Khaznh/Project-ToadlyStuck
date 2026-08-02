using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerInput : MonoBehaviour
{
    public Action OnJumpClicked;
    public Action OnBackClicked;

    public Action<GameObject> OnClickedOnGO;
    public Action<Vector2> OnClickedPos;

    public Vector2 moveVector;
    public bool isShambles = false;

    private Input playerInputAction;

    private void OnEnable()
    {
        playerInputAction = new Input();
        playerInputAction.Enable();

        playerInputAction.NorPlayer.Jump.performed += OnNorJumpPerformed;
        playerInputAction.NorPlayer.Click.performed += OnClick;
        playerInputAction.NorPlayer.Back.performed += Back_performed;

        playerInputAction.ShamblePlayer.Jump.performed += OnShambleJumpPerformed;
    }

    private void Back_performed(InputAction.CallbackContext obj)
    {
        OnBackClicked?.Invoke();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Update()
    { 
        if (isShambles)
        {
            moveVector = playerInputAction.ShamblePlayer.Move.ReadValue<Vector2>();
        } else
        {
            moveVector = playerInputAction.NorPlayer.Move.ReadValue<Vector2>();
        }
    }

    private void OnNorJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isShambles)
        {
            return;
        }

        OnJumpClicked?.Invoke();
    }

    private void OnShambleJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!isShambles)
        {
            return;
        }

        OnJumpClicked?.Invoke();
    }

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 screenPosition = Pointer.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        OnClickedPos?.Invoke(worldPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            OnClickedOnGO?.Invoke(clickedObject);
        }
    }
}
