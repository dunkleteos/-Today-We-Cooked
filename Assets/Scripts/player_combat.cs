using UnityEngine;

public class player_combat : MonoBehaviour
{
    public Animator anim;
    
    [Header("Attack Settings")]
    public Transform attackPoint;    
    public float attackRange = 0.5f; 
    public LayerMask enemyLayers;    
    public int attackDamage = 1;  
    public AudioSource audioSource;
public AudioClip attackSound;   

    public void Attack()
    {
        anim.SetTrigger("AttackTrigger"); 
        anim.SetBool("isAttacking", true);
    }
    public void PlayAttackSound()
{
    if (audioSource != null && attackSound != null)
    {
        audioSource.PlayOneShot(attackSound);
    }
}

    
    public void PerformDamage()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        
        foreach (Collider2D enemy in hitEnemies)
        {
           
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(attackDamage);
                Debug.Log("Düşmana vuruldu!");
            }
        }
    }

    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}