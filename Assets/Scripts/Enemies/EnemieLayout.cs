using System.Collections;
using UnityEngine;

public class EnemieLayout : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;

    public float fadeDuration = 1.0f; // Czas zanikania w sekundach
    public float delayBeforeFade = 2.0f; // Czas opóŸnienia przed zanikiem
    private Renderer objectRenderer;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hitted");
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
    }

    public virtual void attack()
    {

    }

}
