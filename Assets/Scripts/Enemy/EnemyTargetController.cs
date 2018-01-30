using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Game.Building;

public class EnemyTargetController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Health target;

    private Animator animator;
    private Enemy enemy;

    private Health targetHealth;

    bool isAttacking = false;

    bool shouldFollow = true;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        FindTarget();
    }

    void FindTarget()
    {
        if (enemy.targetType == Enemy.Target.Server)
        {
            FindServer();
        }
        else if (enemy.targetType == Enemy.Target.Turret)
        {
            FindBuildings();
        }

        enemy.target = target;
        navAgent.destination = target.transform.position;
    }

    void FindServer()
    {
        target = GlobalController.instance.levelController.server.gameObject.GetComponent<Health>();
    }

    bool FindBuildings()
    {
        var buildings = FindObjectsOfType<Building>().ToList();
        
        if(buildings.Count > 0)
        {
            target = SortTargets(buildings).GetComponent<Health>();
            return true;
        }
        else
        {
            FindServer();
            return false;
        }
    }

    public void StopMoving()
    {
        navAgent.enabled = false;
        shouldFollow = false;
    }

    Building SortTargets(List<Building> targets)
    {
        targets = targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();

        return targets[0];
    }

    private void Update()
    {
        if(shouldFollow)
        {
            if (target != null)
            {
                navAgent.destination = target.transform.position;
            }
            else
            {
                FindBuildings();
            }

            if (navAgent.remainingDistance < navAgent.stoppingDistance + 1f && !isAttacking)
            {
                animator.SetBool("Attack", true);
                isAttacking = true;
                enemy.StartAttacking();
            }
            else if (navAgent.remainingDistance >= navAgent.stoppingDistance + 1f && isAttacking)
            {
                animator.SetBool("Attack", false);
                isAttacking = false;
            }
        }
    }
}