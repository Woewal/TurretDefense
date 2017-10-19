using UnityEngine.AI;
using UnityEngine;
using System.Collections;


public class EnemyNav : MonoBehaviour
{
    public Enemy.Target TargetType;
    private NavMeshAgent NavAgent;
    private GameObject Target;
    private Animator Animator;

    private bool isAttacking = false;

    private void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(GameObject target, Enemy.Target targetType)
    {
        Target = target;
        NavAgent.destination = target.transform.position;
        TargetType = targetType;

        StartCoroutine(Attacking());
    }

    private bool CheckIfArrived()
    {
        if (Target == null)
        {
            return false;
        }

        /*if (!NavAgent.pathPending)
        {
            if (NavAgent.remainingDistance <= NavAgent.stoppingDistance)
            {
                if (!NavAgent.hasPath || NavAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }*/

        if(NavAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Complete");
        }

        return false;
    }

    private IEnumerator Attacking()
    {
        //Animator.SetTrigger("IsAttacking");
        while (Target != null && CheckIfArrived())
        {
            isAttacking = true;
            Debug.Log("Attacking!");
            yield return null;
        }

        isAttacking = false;
    }
}