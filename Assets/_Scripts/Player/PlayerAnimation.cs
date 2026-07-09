using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInput input;
    private Animator animator;

    private void Start()
    {
        input = PlayerController.Instance.playerInput;
        animator = PlayerController.Instance.playerAnimator;
    }

    private void Update()
    {
        HandleMoveAnimation(input.moveVector);
    }

    private void HandleMoveAnimation(Vector2 moveVector)
    {
        int inputX = (int)moveVector.x;
        animator.SetInteger("MoveX", inputX);
    }
}
