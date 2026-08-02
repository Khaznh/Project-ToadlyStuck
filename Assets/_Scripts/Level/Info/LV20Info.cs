using UnityEngine;

public class LV20Info : LVInfo
{
    private bool isOpenning = false;

    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 20);
    }

    private void Update()
    {
        if (isOpenning) return;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenDoor(null);
        }
    }

    private void OpenDoor(GameObject pressGO)
    {
        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        isOpenning = true;

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(21);
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
}
