using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioChannelSO sfxChannel;
    [SerializeField] private AudioChannelSO bgmChannel;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioMixer mainMixer;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);

        sfxChannel.OnPlaySound += PlaySFX;
        sfxChannel.OnStopSound += StopSFX;
        bgmChannel.OnPlaySound += PlayBGM;
        bgmChannel.OnStopSound += StopBGM;
    }

    private void OnDisable()
    {
        sfxChannel.OnPlaySound -= PlaySFX;
        sfxChannel.OnStopSound -= StopSFX;
        bgmChannel.OnPlaySound -= PlayBGM;
        bgmChannel.OnStopSound -= StopBGM;
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private void StopSFX()
    {
        sfxSource.Stop();
    }

    private void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
        bgmSource.loop = true;
    }

    private void StopBGM()
    {
        bgmSource.Stop();
    }
}
