using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class EnemyNavigator : MonoBehaviour
{
    public Enemy.Target targetType;
    private NavMeshAgent navAgent;
    private GameObject target;
    private List<GameObject> targets = new List<GameObject>();
    private Animator animator;
    private Enemy enemy;

    private Health targetHealth;

    bool isAttacking = false;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        FindTargets();
        //SelectTarget();
    }

    void FindTargets()
    {
        if(targetType == Enemy.Target.Server)
        {
            target = GlobalController.instance.levelController.server.gameObject;
            SetTarget(target, targetType);
        }
    }

    void SelectTarget()
    {
        targets = targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();

        SetTarget(targets[0].gameObject, Enemy.Target.Server);
    }

    public void SetTarget(GameObject target, Enemy.Target targetType)
    {
        this.target = target;
        navAgent.destination = target.transform.position;
        this.targetType = targetType;

        if(targetType == Enemy.Target.Server)
        {
            targetHealth = target.GetComponent<Health>();
        }
    }

    private void Update()
    {
        if (navAgent.remainingDistance < navAgent.stoppingDistance + 1f && !isAttacking)
        {
            animator.SetBool("Attack", true);
            isAttacking = true;
        }
        else if (navAgent.remainingDistance >= navAgent.stoppingDistance + 1f && isAttacking)
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
        }
    }

    public void DamageTarget()
    {
        targetHealth.Damage(1f);
    }
}