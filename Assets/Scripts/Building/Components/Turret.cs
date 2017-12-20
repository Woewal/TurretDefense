using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent
{
    /*
    public List<GameObject> Targets = new List<GameObject>();

    public Projectile Projectile;
    public float ShootingInterval;
    public GameObject ProjectileSpawner;

    public ShootingCollider ShootingCollider;

    public float RotationSpeed = 0.4f;

    private GameObject Target;

    private bool IsTargeting = false;*/

    public override void StartComponent()
    {
        //ShootingCollider.IsEnabled = true;
    }
    
    /*
    void SortTargets()
    {
        RemoveNulls();

        Targets = Targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();
    }

    IEnumerator TargetEnemy()
    {
        
        IsTargeting = true;
        StartCoroutine(Shoot());

        while (RemoveNulls() > 0)
        {
            if(Target == null)
            {
                AssignTarget();
            }
            var targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            yield return null;
        }
        IsTargeting = false;
        StartCoroutine(Return());
    }

    private IEnumerator Shoot()
    {
        while (IsTargeting)
        {
            float currentTime = 0;

            while (currentTime < ShootingInterval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            if(IsTargeting)
            {
                Projectile projectile = Instantiate(Projectile).GetComponent<Projectile>();
                projectile.transform.position = ProjectileSpawner.transform.position;
                projectile.InitiateProjectile(Target);
            }
            

            yield return null;
        }
    }

    private IEnumerator Return()
    {
        StopCoroutine(TargetEnemy());
        StopCoroutine(Shoot());
        while (transform.rotation != transform.parent.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, RotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void CheckEnemies()
    {
        if (Targets.Any())
        {
            if (!IsTargeting) {
                AssignTarget();
                StopCoroutine(Return());
                StartCoroutine(TargetEnemy());
            }
        }
        else
        {
            Target = null;
        }
    }

    private void AssignTarget()
    {
        SortTargets();
        Target = Targets[0];
    }

    private int RemoveNulls()
    {
        foreach (GameObject x in Targets.ToList())
        {
            if (x == null)
            {
                Targets.Remove(x);
            }
        }

        return Targets.Count;
    }
    */
}
