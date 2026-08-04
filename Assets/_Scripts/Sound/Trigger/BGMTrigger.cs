using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioChannelSO bgmChannel;

    private void OnEnable()
    {
        bgmChannel.PlaySound(bgmClip);
    }
}
