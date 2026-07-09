using UnityEngine;

public class LV1Info : LVInfo
{
    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        SpawnLevelManager.Instance.SpawnNextLevel();
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

        //doorState = DoorState.Closing;
        //StartCoroutine(DoorAnimationRoutine("GateClosing", "GateCloseIdle", DoorState.Close));
    }
}
