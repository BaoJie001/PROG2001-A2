using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 3;
    public int ScoreNum = 0;
    public bool gameOver;
    public AudioClip getHitSound;
    public GameObject panGameOver;
    public Text txtGameOver;
    public GameObject panGameWin;
    public Text txtGameWin;

    void Update()
    {
        if (gameOver)
            return;

        if (ScoreNum >= 12)
        {
            txtGameWin.text = health.ToString() + "\n" + ScoreNum.ToString();
            panGameWin.SetActive(true);
            gameOver = true;
        }
        if (health <= 0)
        {
            gameOver = true;
            health = 0;
            txtGameOver.text = "SCORE:" + ScoreNum.ToString();
            PanMainGame.Instance.UpadteTxtCount(ScoreNum);
            PanMainGame.Instance.UpadteTxtHeart(health);
            PanMainGame.Instance.ShowUIPanel(EUIPanel.GameOver);
            return;
        }
        PanMainGame.Instance.UpadteTxtCount(ScoreNum);
        PanMainGame.Instance.UpadteTxtHeart(health);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            AudioManager.Instance.PlayOneShotSFX(getHitSound);
            health--;
            Debug.Log(health);
        }
    }
}
