using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour
{
    HealthBar healthBar;

    [Header("Stats")]
    [SerializeField] float maxHealth = 20;
    float currentHealth;

    [Header("Instantiating behaviour")]
    [SerializeField]
    HealthBar healthBarPrefab = null;
    [SerializeField] Transform healthBarParent = null;

    public event Action ZeroHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = Instantiate(healthBarPrefab, healthBarParent);
        healthBar.parent = transform;
    }

    public void Damage(float amount)
    {
        float oldHealth = currentHealth;

        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
            ZeroHealth();
        }
        else
        {
            currentHealth -= amount;
        }

        healthBar.SetHealth(currentHealth / maxHealth);
    }
}
