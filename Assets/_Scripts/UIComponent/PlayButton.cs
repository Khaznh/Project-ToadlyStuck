using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private string moveScene;

    public void Play()
    {
        int lastLevelPlayed = PlayerPrefs.GetInt("LastLevelPlayed", 1);
        LevelChooseData.Instance.levelIndex = lastLevelPlayed;
        SceneTransitionManager.Instance.TransitionToScene(moveScene);
    }
}
