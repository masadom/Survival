using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Camera cam;
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
        cam = Camera.main;
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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);

            float radius = 0.2f;
            Collider2D hit = Physics2D.OverlapCircle(mousePos, radius);

            if (hit != null)
            {
                Interactible interactible = hit.GetComponent<Interactible>();

                if (interactible != null)
                {
                    Debug.Log($"Interakcja z: {interactible.name}");
                    interactible.Focused(gameObject.transform);
                }
            }
            // Nic siê nie dzieje, jeœli hit jest null (czyli nie trafiono w ¿aden obiekt)
        }
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
