// PotionIdleAnimation.cs
using UnityEngine;

public class PotionIdleAnimation : MonoBehaviour
{
    public float rotationSpeed = 0;      // Putaran ramuan
    public float floatSpeed = 2f;           // Kecepatan naik-turun
    public float floatHeight = 0.3f;        // Jarak melayangnya

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Simpan posisi awal
    }

    void Update()
    {
        // Rotasi ramuan
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Gerakan melayang naik-turun
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
