using UnityEngine;

public class FruitApple : MonoBehaviour
{
    public AudioClip getAppleSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlayOneShotSFX(getAppleSFX);
            SheepDarkController.Instance.AddCount(1);
            Destroy(gameObject);
        }
    }
}
