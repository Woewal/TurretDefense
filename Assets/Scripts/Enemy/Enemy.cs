using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;


public class Enemy : MonoBehaviour
{
    public enum Target { Server, Player };

    [HideInInspector] public float health;
    public float maxHealth = 30f;

    [HideInInspector] public HealthBar healthBar;

    EnemyNavigator enemyNav;

    private void Start()
    {
        enemyNav = GetComponent<EnemyNavigator>();

        health = maxHealth;
    }

    public void Reevaluate()
    {

    }
    

    

    public void Damage(float amount)
    {
        float oldHealth = health;

        if (health - amount < 0)
        {
            health = 0;
        }
        else
        {
            health -= amount;
        }

        healthBar.SetHealth(oldHealth, health, maxHealth);

        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        LevelController.instance.EnemySpawnController.Enemies.Remove(this);
        Destroy(gameObject);
        LevelController.instance.EnemySpawnController.CheckIfAllEnemiesAreDead();
    }

    public void SetTarget(Target Target)
    {
        if(Target == Target.Player)
        {

        }
        else if(Target == Target.Server)
        {

        }
    }
}
