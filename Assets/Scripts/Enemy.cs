using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;

    public float fadeDuration = 1.0f; // Czas zanikania w sekundach
    public float delayBeforeFade = 2.0f; // Czas opóŸnienia przed zanikiem
    private Renderer objectRenderer;
    private Color originalColor;


    void Start()
    {

        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);


        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
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
    void Die()
    {
        healthBar.SetHealth(0);
        Debug.Log("die");
        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        FadeOut();
    }


    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetObjectAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetObjectAlpha(0f);
        gameObject.SetActive(false);
    }

    private void SetObjectAlpha(float alpha)
    {
        if (objectRenderer != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha;
            objectRenderer.material.color = newColor;
        }
    }
}
