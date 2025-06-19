using UnityEngine;

public class PotionThrower : MonoBehaviour
{
    public GameObject potionPrefab; 
    public Transform throwPoint; 
    public float throwForce = 10f;

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.potionCount > 0)
    {
        Debug.Log("Spasi ditekan, melempar ramuan!");
        ThrowPotion();
        GameManager.Instance.UsePotion(); // kurangi jumlah potion
    }
}


    void ThrowPotion()
    {
        GameObject potion = Instantiate(potionPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * throwForce; 
    }
}