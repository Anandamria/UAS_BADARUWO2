using UnityEngine;

public class Potion : MonoBehaviour
{
    public AudioClip potionHitSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player menyentuh ramuan");

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(potionHitSFX);
            }
            else
            {
                Debug.Log("AudioManager.instance = null");
            }

            Destroy(gameObject);
        }
    }
}
