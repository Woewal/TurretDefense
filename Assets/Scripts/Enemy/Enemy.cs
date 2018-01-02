using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;


public class Enemy : MonoBehaviour
{
    public enum Target { Server, Player };
    [HideInInspector] public Health health;
    EnemyNavigator enemyNav;

    private void Start()
    {
        enemyNav = GetComponent<EnemyNavigator>();
        health = GetComponent<Health>();
        health.ZeroHealth += Kill;
    }

    void Kill()
    {
        LevelController.instance.EnemySpawnController.Enemies.Remove(this);
        Destroy(gameObject);
        LevelController.instance.EnemySpawnController.CheckIfAllEnemiesAreDead();
    }
}
