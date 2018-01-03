using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<EnemySpawnController>().spawners.Add(this);
    }
}
