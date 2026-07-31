using UnityEngine;

public class LV10Info : LVInfo
{
    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 10);
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(11);
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

    public void OnPlayerColliderPauseButton(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }
}
