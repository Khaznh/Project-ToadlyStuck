using UnityEngine;
using UnityEngine.Audio;

public class LV19Info : LVInfo
{
    [SerializeField] private AudioMixer mainMixer;

    private bool isOpenning = false;

    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 19);
    }

    private void Update()
    {
        if (isOpenning) return;

        if (IsBothSoundTurnOff())
        {
            isOpenning = true;
            OpenDoor(null);
        }
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(20);
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

    private bool IsBothSoundTurnOff()
    {
        float sfxVolume;
        bool sfxResult = mainMixer.GetFloat("SFX", out sfxVolume);

        float bgmVolume;
        bool bgmResult = mainMixer.GetFloat("BGM", out bgmVolume);

        if (sfxResult && bgmResult)
        {
            if (sfxVolume <= -80f && bgmVolume <= -80f)
            {
                return true;
            }
        }

        return false;
    }

    private void OpenDoor(GameObject pressGO)
    {
        if (doorState == DoorState.Open || doorState == DoorState.Opening)
        {
            return;
        }

        doorState = DoorState.Opening;
        StartCoroutine(DoorAnimationRoutine("GateOpenning", "GateOpenIdle", DoorState.Open));
    }
}
