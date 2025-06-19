using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float leftBound = -20f; // batas kiri di mana enemy akan dihancurkan

    void Update()
    {
        
        if (this != null)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x < leftBound)
            {
                Destroy(gameObject); // Hancurkan musuh jika terlalu kiri
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log("Kena player!");

            Destroy(gameObject); // Hancurkan musuh setelah kena player
        }
    }

    // visualisasi batas di Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBound, -10f, 0), new Vector3(leftBound, 10f, 0));
    }
}
