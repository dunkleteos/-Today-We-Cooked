using UnityEngine;

public class stage1Movement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    float vertical;
    float horizontal;
    public Animator animator;
    public int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(horizontal, vertical)    * speed;
        if(horizontal>0 && transform.localScale.x<0 || horizontal < 0 && transform.localScale.x >0 ){Flip();}
        animator.SetFloat("horizontal",Mathf.Abs(horizontal));
        animator.SetFloat("vertical",Mathf.Abs(vertical));
    }
    void Flip(){
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }

}
