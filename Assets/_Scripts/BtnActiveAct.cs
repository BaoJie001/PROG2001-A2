using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BtnActiveAct : MonoBehaviour
{
    public bool isBtnSelf = true;
    public Button btnActive;

    [Header("按钮点击事件")]
    public UnityEvent onBtnClickEvt;

    [Header("失活的游戏对象")]
    public GameObject[] disableObjLst;

    [Header("需要激活的游戏对象")]
    public GameObject[] enableObjLst;

    private void Start()
    {
        if (isBtnSelf)
            btnActive = GetComponent<Button>();
        if (!btnActive)
            return;
        btnActive.onClick.AddListener(() =>
        {
            onBtnClickEvt?.Invoke();
            if (disableObjLst.Length > 0)
            {
                foreach (GameObject disobj in disableObjLst)
                    disobj.SetActive(false);
            }
            if (enableObjLst.Length > 0)
            {
                foreach (GameObject obj in enableObjLst)
                    obj.SetActive(true);
            }
        });
    }
}
