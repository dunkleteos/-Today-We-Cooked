using UnityEngine;
using System.Collections; // Coroutine (FlashRoutine) için gerekli

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public Transform player;

    [Header("Attack Settings")]
    public int damage = 1;
    public float attackCooldown = 1.5f;
    public Transform attackPoint;    // Burası boş kalmamalı (Inspector'dan ata)
    public float attackRadius = 0.5f; // Saldırı dairesinin büyüklüğü
    public LayerMask playerLayer;    // "Player" layer'ını seçmelisin

    [Header("Health & Effects")]
    public int health = 1;
    public float destroyDelay = 0.5f; // Ölürken bekleme süresi
    public Material flashMaterial;      // Beyaz shader içeren materyal
    public float flashDuration = 0.1f;  // Beyaz kalma süresi

    [Header("Audio")]
    public AudioSource enemyAudioSource;
    public AudioClip deathClip;

    private float attackTimer;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Orijinal materyali kaydet
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }

        // Oyuncuyu etiketle (Tag) otomatik bul
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        Flip();
    }

    void FixedUpdate()
    {
        if (player == null || isDead) return;

        // Oyuncuya doğru hareket et
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    // Trigger alanına oyuncu girdiğinde saldırı başlat
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player") && attackTimer <= 0f)
        {
            Attack();
        }
    }

    void Attack()
    {
        attackTimer = attackCooldown;
        if (animator != null) animator.SetTrigger("Attack");
        rb.linearVelocity = Vector2.zero; // Saldırırken dur
    }

    // --- CAN VE HASAR SİSTEMİ ---
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;
        StartCoroutine(FlashRoutine()); // Beyaz flaş efektini başlat

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRoutine()
    {
        if (spriteRenderer != null && flashMaterial != null)
        {
            spriteRenderer.material = flashMaterial; // Beyaza dön
            yield return new WaitForSeconds(flashDuration);
            if (!isDead)
            {
                spriteRenderer.material = originalMaterial; // Eski haline dön
            }
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (enemyAudioSource != null && deathClip != null)
    {
        enemyAudioSource.PlayOneShot(deathClip);
    }
        
        rb.linearVelocity = Vector2.zero;

        // Ölürken kalıcı beyaz kalsın
        if (spriteRenderer != null && flashMaterial != null)
        {
            spriteRenderer.material = flashMaterial;
        }

        // Spawner'a haber ver (Bölüm bitişi için)
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.EnemyDied();
        }

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Destroy(gameObject, destroyDelay);
    }

    // --- ANİMASYON EVENT İÇİN ---
    public void DealDamage()
    {
        if (attackPoint == null || isDead) return;

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (hitPlayer != null)
        {
            // Oyuncunun can sistemine ulaş (HealthSystem ismini kontrol et)
            if (hitPlayer.TryGetComponent<HealthSystem>(out HealthSystem healthSys))
            {
                healthSys.ChangeHealth(-damage);
            }
        }
    }

    void Flip()
    {
        if (player == null) return;

        // Oyuncu sağdaysa normal ölçek, soldaysa ters ölçek
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Editörde saldırı alanını kırmızı bir daire olarak görmek için
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}