using UnityEngine;

public class LV12Info : LVInfo
{
    [SerializeField] private int timeKnockDoor = 2;

    private int currentTimeKnock = 0;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 12);
        PlayerController.Instance.playerInput.OnClickedOnGO += PressOnDoor;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerInput.OnClickedOnGO -= PressOnDoor;
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        LevelUnlockManager.Instance.UnlockNextLevel(13);
        SpawnLevelManager.Instance.SpawnNextLevel();
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

    private void PressOnDoor(GameObject go)
    {
        if (!go.CompareTag("LV12Door"))
        {
            return;
        }

        //Raise event audio knock door
        DoorOpen();
    }

    private void DoorOpen()
    {
        currentTimeKnock++;

        if (currentTimeKnock == timeKnockDoor)
        {
            sfx.PlaySound(buttonClick);

            if (doorState == DoorState.Open || doorState == DoorState.Opening)
            {
                return;
            }
            doorState = DoorState.Opening;
            StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
        }
    }
}
