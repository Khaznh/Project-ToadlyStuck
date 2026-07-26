using UnityEngine;

public class LV9Info : LVInfo
{
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float flySpeed = 5f;

    [SerializeField] private Vector2 targetPos;

    private void OnEnable()
    {
        PlayerController.Instance.playerInput.OnClickedPos += GetTargetPos;
        PlayerController.Instance.playerMovement.enabled = false;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerInput.OnClickedPos -= GetTargetPos;
        PlayerController.Instance.playerMovement.enabled = true;
    }

    private void GetTargetPos(Vector2 targetVec)
    {
        targetPos = targetVec;
    }

    private void Update()
    {
        if (targetPos == Vector2.zero)
        {
            return;
        }

        Vector2 currentPos = PlayerController.Instance.playerRigidbody.position;
        Vector2 dir = targetPos - currentPos;

        if (dir.magnitude > stopDistance)
        {
            PlayerController.Instance.playerRigidbody.linearVelocity = dir.normalized * flySpeed;
        } else
        {
            PlayerController.Instance.playerRigidbody.linearVelocity = Vector2.zero;
            targetPos = Vector2.zero;
        }
    }


    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        SpawnLevelManager.Instance.SpawnNextLevel();
    }

    public void OnPlayerPreviousLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        SpawnLevelManager.Instance.SpawnPreviousLevel();
    }

    public void OnPlayerPressButton(Collider2D collision, Activer activer)
    {
        PressButton(collision.gameObject);
    }

    public void OnPlayerLeaveButton(Collider2D collision, Activer activer)
    {
        LeaveButton(collision.gameObject);
    }

    public void OnPlayerSpike(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        PlayerController.Instance.playerDeath.Die();
        PlayerController.Instance.transform.position = playerSpawn.position;
    }

    private void PressButton(GameObject pressGO)
    {
        if (!pressGO.gameObject.CompareTag("Player"))
        {
            return;
        }

        buttonState = ButtonState.Pressing;
        StartCoroutine(ButtonAnimationRoutine("RedButtonOpenning", "RedButtonOnIdle", ButtonState.Pressed));

        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }

    private void LeaveButton(GameObject pressGO)
    {
        if (!pressGO.gameObject.CompareTag("Player"))
        {
            return;
        }

        buttonState = ButtonState.Unpressing;
        StartCoroutine(ButtonAnimationRoutine("RedButtonClosing", "RedButtonOffIdle", ButtonState.Unpressed));
    }
}
