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

    public DroppableItems droppableItems;

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

        Pickup drop = Instantiate(droppableItems.battery);
        drop.transform.position = transform.position;
        drop.transform.rotation = transform.rotation;

        Destroy(gameObject);
        GlobalController.instance.levelController.CheckRemainingEnemies();

        GlobalController.instance.levelController.AddScrap(15);
        
    }
}
