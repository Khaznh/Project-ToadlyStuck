using UnityEngine;

public class LV27Info : LVInfo
{
    [SerializeField] private LV27OpenDoorCanvas lV27OpenDoorCanvas;

    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 27);
        PlayerController.Instance.playerInput.OnClickedOnGO += CheckCondition;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerInput.OnClickedOnGO -= CheckCondition;
    }

    private void CheckCondition(GameObject go)
    {
        if (!go.CompareTag("LV27Door"))
        {
            return;
        }

        lV27OpenDoorCanvas.gameObject.SetActive(true);
    }

    public void Accept()
    {
        PressButton(null);
        lV27OpenDoorCanvas.gameObject.SetActive(false);
    }

    public void Decline()
    {
        lV27OpenDoorCanvas.gameObject.SetActive(false);
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(28);
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
