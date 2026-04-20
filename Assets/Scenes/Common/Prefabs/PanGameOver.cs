using UnityEngine;
using UnityEngine.UI;

public class PanGameOver : MonoBehaviour
{
    public Text txtTip;

    public AudioClip GameOverSound;

    void OnEnable()
    {
        AudioManager.Instance.PlayOneShotSFX(GameOverSound);
        if (SheepDarkController.Instance != null)
            txtTip.text = "SCORE:" + SheepDarkController.Instance.ScoreCount.ToString();
    }
}
