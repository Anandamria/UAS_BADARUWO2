using UnityEngine;

public class CameraFollowXOnly : MonoBehaviour
{
    public Transform target; // Referensi ke Player
    public float smoothSpeed = 0.125f;
    public float xOffset = 0f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Simpan posisi Y dan Z awal kamera
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Hanya mengikuti posisi X dari player
        float desiredX = target.position.x + xOffset;
        Vector3 desiredPosition = new Vector3(desiredX, fixedY, fixedZ);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
