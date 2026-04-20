using UnityEngine;
using UnityEngine.UI;

public class PanMainGame : MonoBehaviour
{
    [SerializeField]
    private Button btnPause;

    [SerializeField]
    private Text TxtHeart;

    [SerializeField]
    private Text TxtCount;

    [SerializeField]
    private AudioClip mainBGM;

    public GameObject[] panPanels;

    public static PanMainGame Instance;

    void Awake() => Instance = this;

    void OnDestroy() => Instance = null;

    void Start()
    {
        AudioManager.Instance.PlayGmBGM(mainBGM);
        btnPause.onClick.AddListener(() => ShowUIPanel(EUIPanel.GamePause));
    }

    public void ShowUIPanel(EUIPanel panel) => panPanels[(int)panel].SetActive(true);

    public void UpadteTxtHeart(int heart) => TxtHeart.text = heart.ToString();

    public void UpadteTxtCount(int count) => TxtCount.text = count.ToString();
}

public enum EUIPanel
{
    GamePause,
    GameOver,
    GameWin,
}
