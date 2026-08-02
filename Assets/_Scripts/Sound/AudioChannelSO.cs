using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioChannel", menuName = "Audio/AudioChannel")]
public class AudioChannelSO : ScriptableObject
{
    public Action<AudioClip> OnPlaySound;
    public Action OnStopSound;

    public void PlaySound(AudioClip clip)
    {
        OnPlaySound?.Invoke(clip);
    }

    public void TurnOff()
    {
        OnStopSound?.Invoke();
    }
}
