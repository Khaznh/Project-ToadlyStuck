using System;
using UnityEngine;

public class LV26Info : LVInfo
{
    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 26);
        PlayerController.Instance.playerInput.OnBackClicked += OnBackClicked;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerInput.OnBackClicked -= OnBackClicked;
    }

    private void OnBackClicked()
    {
        PressButton(null);
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(27);
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

    private void PressButton(GameObject pressGO)
    {
        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }
}
