using UnityEngine;
using UnityEngine.UI;

public class PanGameWin : MonoBehaviour
{
    public Text txtTip;

    public AudioClip GameWinSound;

    void OnEnable()
    {
        AudioManager.Instance.PlayOneShotSFX(GameWinSound);
        txtTip.text =
            SheepDarkController.Instance.CurHP.ToString()
            + "\n"
            + SheepDarkController.Instance.ScoreCount.ToString();
    }
}
