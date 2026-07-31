using UnityEngine;

public class LevelDisableButton : MonoBehaviour
{
    [SerializeField] private GameObject chooseLevelCanvas;

    public void OnClickButton()
    {
        chooseLevelCanvas.SetActive(false);
    }
}
