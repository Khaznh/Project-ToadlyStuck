using UnityEngine;

public class LevelEneableButton : MonoBehaviour
{
    [SerializeField] private GameObject chooseLevelCanvas;

    public void OnClickButton()
    {
        chooseLevelCanvas.SetActive(true);
    }
}
