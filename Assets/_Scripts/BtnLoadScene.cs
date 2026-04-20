using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnLoadScene : MonoBehaviour
{
    public bool isSelfBtn = true;
    public string nextScene;

    private Button btnLoad;

    private void Awake()
    {
        if (isSelfBtn)
            btnLoad = GetComponent<Button>();
        if (btnLoad != null)
            btnLoad.onClick.AddListener(LoadNextScene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
