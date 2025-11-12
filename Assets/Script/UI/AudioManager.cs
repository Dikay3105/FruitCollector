using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgmSource;
    public Slider volumeSlider;

    [Header("BGM Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Nếu chưa có AudioSource thì báo lỗi
        if (bgmSource == null)
        {
            Debug.LogError("⚠️ Chưa gán AudioSource cho AudioManager!");
            return;
        }

        // Đặt âm lượng mặc định hoặc lấy từ lưu
        float savedVol = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        bgmSource.volume = savedVol;
        bgmSource.loop = true;

        // Bắt đầu phát nhạc
        if (!bgmSource.isPlaying)
            bgmSource.Play();

        // Nếu có slider → gán sự kiện
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = savedVol;
            volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (bgmSource == null) return;
        bgmSource.volume = value;

        // KHÔNG được gọi Play() lại — vì sẽ restart bài nhạc!
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource == null || clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying) return; // đang phát rồi thì không restart

        bgmSource.clip = clip;
        bgmSource.Play();
    }

}
