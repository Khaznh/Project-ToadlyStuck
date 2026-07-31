using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Image buttonImage; 

    [SerializeField] private string goToScene;

    private Color unlockedColor= Color.white;
    private Color lockedColor= Color.gray;

    public void Init(int levelIndex)
    {
        levelText.text = levelIndex.ToString();

        if (LevelUnlockManager.Instance.IsLevelUnlocked(levelIndex))
        {
            buttonImage.color = unlockedColor;
        } else
        {
            buttonImage.color = lockedColor;
        }
    }

    public void GoToLevel()
    {
        int goToLevel = 1;
        
        if (int.TryParse(levelText.text, out goToLevel))
        {
            if (LevelUnlockManager.Instance.IsLevelUnlocked(goToLevel))
            {
                LevelChooseData.Instance.levelIndex = goToLevel;
                SceneTransitionManager.Instance.TransitionToScene(goToScene);
            }
        }
    }
}
