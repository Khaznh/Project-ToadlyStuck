using UnityEngine;

public class CameraCanva : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas not found");
            return;
        }

        canvas.worldCamera = Camera.main;
    }

    public void PauseGameClick()
    {

    }

    public void SuggestionClick()
    {

    }
}
