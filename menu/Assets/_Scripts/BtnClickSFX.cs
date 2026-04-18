using UnityEngine;
using UnityEngine.UI;

public class BtnClickSFX : MonoBehaviour
{
    private Button btnClick;

    void Start()
    {
        btnClick = GetComponent<Button>();
        btnClick.onClick.AddListener(() => AudioManager.Instance.PlayClickSfx());
    }
}
