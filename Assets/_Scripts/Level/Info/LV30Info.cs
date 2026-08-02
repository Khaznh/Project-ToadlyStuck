using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LV30Info : LVInfo
{
    [SerializeField] private GameObject message;

    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 30);
        PlayerController.Instance.playerInput.enabled = false;
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(5f);
        message.transform.localScale = Vector3.zero;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(message.transform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutBack));
        mySequence.AppendInterval(10f);
        mySequence.Append(message.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InBack));
        mySequence.OnComplete(() =>
        {
            message.gameObject.SetActive(false);
        });
        yield return new WaitForSeconds(10f);
        SceneTransitionManager.Instance.TransitionToScene("MenuScene");
    }
}
