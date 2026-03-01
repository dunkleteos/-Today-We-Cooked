using UnityEngine;

public class PlatformJump : MonoBehaviour
{
    public float jumpForce = 12f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Şart: Çarpan şeyin hızı aşağı doğru olmalı (Düşüyor olmalı)
        // 2. Şart: Çarpan objenin Rigidbody'si olmalı
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Mevcut hızı sıfırla ki zıplama gücü hep aynı kalsın
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }
}