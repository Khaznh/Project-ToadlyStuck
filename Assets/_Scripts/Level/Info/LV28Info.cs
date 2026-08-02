using UnityEngine;

public class LV28Info : LVInfo
{
    [SerializeField] private GameObject suggestCanva;

    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 28);
    }

    public void OpenCanva()
    {
        suggestCanva.SetActive(true);
    }

    public void CloseCanva()
    {
        PressButton(null);
        suggestCanva.SetActive(false);
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(29);
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
