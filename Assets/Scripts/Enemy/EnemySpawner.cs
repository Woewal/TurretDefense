using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public void Start()
    {
        GlobalController.instance.levelController.enemyManager.enemySpawners.Add(this);
    }
}
