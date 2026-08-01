using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioButtonToggle : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private string namePara;
    [SerializeField] private AudioMixer mainMixer;

    private Color normalColor = Color.white;
    private Color disableColor = Color.gray;

    public void Toggle()
    {
        float currentVolume;
        bool result = mainMixer.GetFloat(namePara, out currentVolume);
        if (result)
        {
            if (currentVolume <= -80f)
            {
                mainMixer.SetFloat(namePara, 0f);
            }
            else
            {
                mainMixer.SetFloat(namePara, -80f);
            }
        }
    }

    private bool IsMute()
    {
        float currentVolume;

        bool result = mainMixer.GetFloat(namePara, out currentVolume);

        if (result)
        {
            if (currentVolume <= -80f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void Update()
    {
        if (IsMute())
        {
            buttonImage.color = disableColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
    }
}
