using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerInput : MonoBehaviour
{
    public Action OnJumpClicked;
    public Action<GameObject> OnClickedOn;

    public Vector2 moveVector;
    public bool isShambles = false;

    private Input playerInputAction;

    private void OnEnable()
    {
        playerInputAction = new Input();
        playerInputAction.Enable();
        playerInputAction.NorPlayer.Jump.performed += OnJumpPerformed;
        playerInputAction.NorPlayer.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Update()
    {
        moveVector = playerInputAction.NorPlayer.Move.ReadValue<Vector2>();
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnJumpClicked?.Invoke();
    }

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 screenPosition = Pointer.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            OnClickedOn?.Invoke(clickedObject);
        }
    }
}
