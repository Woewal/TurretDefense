using UnityEngine;
using System.Collections;
using Game.Building;

public class Demolisher : Enemy
{
    public GameObject explosionPrefab;

    void Start()
    {
        Initiate();
    }

    public override void Attack()
    {
        DestroyNearbyTurrets();

        Kill();
    }

    void DestroyNearbyTurrets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3);

        foreach (var collider in colliders)
        {
            var health = collider.gameObject.GetComponent<Health>();

            if (health != null)
            {
                health.Damage(damage);
            }
        }

        var explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
    }

    public override void StartAttacking()
    {
        targetController.StopMoving();
    }
}
