using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gunderuwo"))
        {
            Debug.Log("Ramuan kena Uwo!");
            
            // Panggil fungsi TakeHit jika ada di Gunderuwo
            GunderuwoController gunderuwo = other.GetComponent<GunderuwoController>();
            if (gunderuwo != null)
            {
                gunderuwo.TakeHit(); // Panggil method yang mengurangi HP
            }

            Destroy(gameObject); // Hancurkan ramuan
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); // Hancurkan ramuan kalau kena benda lain
        }
    }
}