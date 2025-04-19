using UnityEngine;

public class Skeleton : EnemyController
{
    bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;

    public GameObject attackPoint;
    


    //attack
    public int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
       


    }
    public override void Update()
    {
        base.Update();
        if (transform.position.x > target.position.x)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    public override void Attack()
    {
        

        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + 0.7f;

            base.Attack();
            animator.SetTrigger("Attacks");
        }
    }

    
    public void DealDamage()
    {
        nextAttackTime = Time.time + 1f / attackRate;
        player.GetComponent<PlayerBehaviour>().TakeDamage(attackDamage);
    }
    public override void Die()
    {
        base.Die();
        FadeOut();

    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        isFacingRight = !isFacingRight;
        float xValue = attackPoint.transform.localPosition.x * -1;
        float yValue = attackPoint.transform.localPosition.y;
        float zValue = attackPoint.transform.localPosition.z;
        attackPoint.transform.localPosition = new Vector3(xValue, yValue, zValue);
    }
    
}
