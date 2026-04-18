using UnityEngine;

public class PanInterface : MonoBehaviour
{
    public AudioClip startBGM;

    void Start()
    {
        if (AudioManager.Instance.AudioGm.clip == null || !AudioManager.Instance.AudioGm.isPlaying)
            AudioManager.Instance.PlayGmBGM(startBGM);
    }
}
