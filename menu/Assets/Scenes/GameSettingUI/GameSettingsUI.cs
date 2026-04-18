using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    public Slider sliderVolume;
    public Button btnOpen;
    public Button btnSave;

    public string nextSceneName;

    void Start()
    {
        sliderVolume.onValueChanged.AddListener(v =>
        {
            AudioManager.Instance.SetGmVolume(v);
        });

        btnOpen.onClick.AddListener(() =>
        {
            AudioManager.Instance.ToggleGmBGM();
        });

        btnSave.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextSceneName);
        });
    }
}
