using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public enum Target { Server, Player };
public class Enemy : MonoBehaviour
{
    public float Health;
    public float MaxHealth = 30f;

    private NavMeshAgent NMA;

    public Image HealthBar;

    private List<Server> Targets;

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>();

        Targets = new List<Server>();
        foreach (Server server in FieldController.instance.Servers)
        {
            Targets.Add(server);
        }

        Health = MaxHealth;

        FindTarget();
    }

    private void Update()
    {
        if (!NMA.pathPending)
        {
            if (NMA.remainingDistance <= NMA.stoppingDistance)
            {
                if (!NMA.hasPath || NMA.velocity.sqrMagnitude == 0f)
                {

                }
            }
        }
    }

    void FindTarget()
    {
        Targets = Targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();

        NMA.destination = Targets[0].transform.position;
    }

    public void Damage(float amount)
    {
        StartCoroutine(ChangeHealthBar(Health, Health - amount));
        Health -= amount;
        CheckHealth();
    }

    IEnumerator ChangeHealthBar(float oldHealth, float newHealth)
    {
        float currentTime = 0;
        float endTime = 0.2f;

        while (currentTime < endTime)
        {
            HealthBar.fillAmount = Mathf.Lerp(oldHealth / MaxHealth, newHealth / MaxHealth, currentTime / endTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

    }

    void CheckHealth()
    {
        if (Health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        FieldController.instance.EnemySpawnController.Enemies.Remove(this);
        Destroy(gameObject);
        FieldController.instance.EnemySpawnController.CheckIfAllEnemiesAreDead();
    }
}
