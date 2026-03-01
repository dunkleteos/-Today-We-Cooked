using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    public int Current_Health;
    public int max_Health = 5;

    [Header("Flash Ayarları")]
    public SpriteRenderer spriteRenderer;
    public Material flashMaterial;    // Beyaz olan materyal (EnemyFlashMaterial)
    public float flashDuration = 0.1f;

    private Material originalMaterial; // Player'ın kendi normal materyali
    private bool isDead = false;

    void Start()
    {
        Current_Health = max_Health;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Oyun başında Player'ın orijinal materyalini hafızaya al
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (isDead) return;

        Current_Health += amount;
        Current_Health = Mathf.Clamp(Current_Health, 0, max_Health);

        // Hasar aldıysa flaş efektini başlat
        if (amount < 0 && Current_Health > 0)
        {
            StartCoroutine(FlashRoutine());
        }

        if (Current_Health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRoutine()
    {
        if (spriteRenderer != null && flashMaterial != null)
        {
            spriteRenderer.material = flashMaterial; // Beyaz materyali tak
            yield return new WaitForSeconds(flashDuration);
            
            if (!isDead)
            {
                spriteRenderer.material = originalMaterial; // Kendi materyaline dön
            }
        }
    }

    void Die()
    {
        isDead = true;
        SceneManager.LoadScene("GameOverArena");
    }
}