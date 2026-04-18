using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip clickSFX;

    public AudioSource AudioGm => audioGame;
    public static AudioManager Instance { get; private set; }

    protected void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        audioGame = GetComponent<AudioSource>();
    }

    private AudioSource audioGame;

    public void SetGmVolume(float volume)
    {
        audioGame.volume = volume;
    }

    public void PlayGmBGM(AudioClip bgmClip)
    {
        audioGame.clip = bgmClip;
        audioGame.Play();
        audioGame.loop = true;
    }

    public void PlayOneShotSFX(AudioClip sfxClip)
    {
        audioGame.PlayOneShot(sfxClip);
    }

    public void ToggleGmBGM()
    {
        audioGame.mute = !audioGame.mute;
    }

    internal void PlayClickSfx() => audioGame.PlayOneShot(clickSFX);
}
