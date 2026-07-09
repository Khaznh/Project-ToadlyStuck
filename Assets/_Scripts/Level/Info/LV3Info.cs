using Unity.VisualScripting;
using UnityEngine;

public class LV3Info : LVInfo
{
    private void OnEnable()
    {
        PlayerController.Instance.playerInput.OnClickedOn += PressOnButton;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerInput.OnClickedOn -= PressOnButton;
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

    private void PressOnButton(GameObject go)
    {
        if (!go.gameObject.CompareTag("LV3Button"))
        {
            return;
        }

        PressButton(go);
    }

    private void PressButton(GameObject pressGO)
    {
        buttonState = ButtonState.Pressing;
        StartCoroutine(ButtonAnimationRoutine("RedButtonOpenning", "RedButtonOnIdle", ButtonState.Pressed));

        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }
}
