using UnityEngine;

public class PickUpPear : MonoBehaviour
{
    public AudioClip getPickUpSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.Instance.PlayOneShotSFX(getPickUpSound);
            Player player = other.gameObject.GetComponentInParent<Player>();
            player.ScoreNum++;
            Destroy(gameObject);
        }
    }
}
