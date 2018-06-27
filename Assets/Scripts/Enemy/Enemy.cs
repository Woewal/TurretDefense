using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
using Game.Building;

[RequireComponent(typeof(EnemyTargetController))]
public abstract class Enemy : MonoBehaviour
{
    public enum Target { Server, Player, Turret };
    [HideInInspector] public Health health;

    public Target targetType;
    [HideInInspector]public Health target;

    public float damage;

    [HideInInspector] public EnemyTargetController targetController;

    public DroppableItems droppableItems;

    private void Start()
    {
        
    }

    public void Initiate()
    {
        targetController = GetComponent<EnemyTargetController>();
        health = GetComponent<Health>();
        health.ZeroHealth += Kill;
    }

    public abstract void Attack();

    internal void Kill()
    {
        GlobalController.instance.levelController.enemyManager.waveEnemies.Remove(this);
        GlobalController.instance.levelController.enemyManager.CheckWinningStatus();

        Destroy(gameObject);
    }

    public virtual void StartAttacking()
    {

    }
}
