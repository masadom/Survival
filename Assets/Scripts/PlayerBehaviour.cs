using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;

    public int maxHealth = 100;
    private int currentHealth;

    Animator animator;
    public HealthBarScript healthBar;


    private Vector2 movementDirection;

    bool isFacingRight = true;
    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * movementSpeed;

        if (rb.linearVelocity.x>0 && !isFacingRight)
        {
            Flip();
        }
        if (rb.linearVelocity.x<0 && isFacingRight)
        {
            Flip();
        }

        if (rb.linearVelocity.x == 0)
        {
            animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.y));

        }
        else
        {
            animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        }
    }


    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        isFacingRight = !isFacingRight;

    }






    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //animator.SetTrigger("Hitted");
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("playerdied");
            //Die();
        }
    }
}
