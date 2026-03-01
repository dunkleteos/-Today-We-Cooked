using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player
    public float smoothSpeed = 0.125f; // Takip yumuşaklığı (İstersen artırabilirsin)
    public Vector3 offset = new Vector3(0, 0, -10); // Kamera mesafesi

    void LateUpdate()
    {
        if (target != null)
        {
            // Sadece Y ekseninde takip et, X sabit kalsın (veya X'i de target.position.x yapabilirsin)
            Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, offset.z);
            
            // Yumuşak takip için Lerp kullanıyoruz (Kamera daha klas hareket eder)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}