using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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

    public GameObject attackPoint;


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
        if (IsPointerOverUIElement() && !IsPointerOverHealthBar())
        {
            movementDirection = Vector2.zero;
            return;
        }
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

    public void GetDamage(int amount)
    {
        currentHealth-=amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("die");
    }
    
       

    // Sprawdza, czy kursor jest nad UI
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Sprawdza, czy kursor jest nad healthBarem
    private bool IsPointerOverHealthBar()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("healthBar")) // Upewnij siê, ¿e healthBar ma przypisany tag w Unity
            {
                return true;
            }
        }

        return false;
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
        float xValue = attackPoint.transform.localPosition.x * -1;
        float yValue = attackPoint.transform.localPosition.y;
        float zValue = attackPoint.transform.localPosition.z;
        attackPoint.transform.localPosition = new Vector3(xValue, yValue, zValue);
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
