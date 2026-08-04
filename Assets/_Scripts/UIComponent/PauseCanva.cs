using DG.Tweening;
using UnityEngine;

public class PauseCanva : MonoBehaviour
{
    [SerializeField] private Vector3 showPos = new Vector3(0, -150f, 0);
    [SerializeField] private Vector3 hidePos = new Vector3(0, 150f, 0);

    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private RectTransform content;

    private void OnEnable()
    {
        content.position = hidePos;
        Time.timeScale = 0f;
        ShowPauseCanva();
    }

    public void ShowPauseCanva()
    {
        content.DOKill();
        content.DOAnchorPos(showPos, 0.2f)
            .SetUpdate(true);
    }

    public void HidePauseCanva()
    {
        content.transform.DOKill();
        content.DOAnchorPos(hidePos, moveDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            });
    }

    public void ResumeGameClick()
    {
        HidePauseCanva();
    }

    public void QuitGameClick()
    {
        Time.timeScale = 1f;
        SceneTransitionManager.Instance.TransitionToScene("MenuScene");
    }

    private void OnDisable()
    {
        content.transform.DOKill();
    }
}
