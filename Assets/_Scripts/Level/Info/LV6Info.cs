using UnityEngine;

public class LV6Info : LVInfo
{
    private void OnEnable()
    {
        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
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

    public void OnPlayerSpike(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        PlayerController.Instance.playerDeath.Die();
        PlayerController.Instance.transform.position = playerSpawn.position;
    }

    public void OnPlayerPressButton(Collider2D collision, Activer activer)
    {
        PressButton(collision.gameObject);
    }

    public void OnPlayerLeaveButton(Collider2D collision, Activer activer)
    {
        LeaveButton(collision.gameObject);
    }

    private void PressButton(GameObject pressGO)
    {
        if (!pressGO.gameObject.CompareTag("Player"))
        {
            return;
        }

        buttonState = ButtonState.Pressing;
        StartCoroutine(ButtonAnimationRoutine("RedButtonOpenning", "RedButtonOnIdle", ButtonState.Pressed));

        if (doorState == DoorState.Close || doorState == DoorState.Closing)
        {
            return;
        }

        doorState = DoorState.Closing;
        StartCoroutine(DoorAnimationRoutine("GateClosing", "GateCloseIdle", DoorState.Close));
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
