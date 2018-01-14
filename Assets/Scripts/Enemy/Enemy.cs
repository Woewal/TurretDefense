using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
using Game.Building;

public class Enemy : MonoBehaviour
{
    public enum Target { Server, Player };
    [HideInInspector] public Health health;
    EnemyNavigator enemyNav;

    public List<Building> TargetedByBuildings;

    private void Start()
    {
        
    }

    public void Initiate()
    {
        enemyNav = GetComponent<EnemyNavigator>();
        health = GetComponent<Health>();
        health.ZeroHealth += Kill;
    }

    void Kill()
    {
        GlobalController.instance.levelController.remainingEnemies.Remove(this);
        Destroy(gameObject);
        GlobalController.instance.levelController.CheckRemainingEnemies();

        foreach(Building building in TargetedByBuildings)
        {
            building.CheckTargets();
        }
    }
}
