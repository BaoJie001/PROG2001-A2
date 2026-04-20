using UnityEngine;

public class TomatoItem : MonoBehaviour
{
    public AudioClip TomatoSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayOneShotSFX(TomatoSound);
            DeerManager.tomatoItemCount++;
            Destroy(gameObject);
        }
    }
}
