using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource; // gán AudioSource này
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;

    private void Start()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", bgmVolume);
        }
    }

    // Hàm chỉnh âm lượng từ Slider
    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
            PlayerPrefs.SetFloat("BGMVolume", volume);
            PlayerPrefs.Save();
        }
    }
}
