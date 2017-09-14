using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent
{
    public List<GameObject> Targets;

    private Coroutine TargetingCoroutine;
    private Coroutine ShootingCoroutine;
    private Coroutine ReturningCoroutine;

    public Projectile Projectile;
    public float ShootingInterval;
    public GameObject ProjectileSpawner;

    private Quaternion DefaultRotation;

    public float RotationSpeed = 7f;

    void Start()
    {
        InitializeComponent();
        DefaultRotation = transform.rotation;
        Targets = new List<GameObject>();
    }

    public void CheckTargets()
    {
        if (Targets.Count != 0)
        {
            if (TargetingCoroutine == null)
            {
                if (ReturningCoroutine != null)
                {
                    StopCoroutine(ReturningCoroutine);
                }
                TargetingCoroutine = StartCoroutine(Target());
            }
        }
        else
        {
            ReturningCoroutine = StartCoroutine(Return());
        }
    }

    void SortTargets()
    {
        Targets = Targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();
    }

    void TargetEnemy(GameObject enemy)
    {
        var targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    private IEnumerator Target()
    {
        ShootingCoroutine = StartCoroutine(Shoot());
        while (base.Building.On)
        {
            RemoveNulls();
            if (Targets.Any())
            {
                if (Targets.Count > 1)
                {
                    SortTargets();
                }
                if (Targets[0] != null)
                {
                    TargetEnemy(Targets[0]);
                }
                else
                {
                    StopCoroutine(ShootingCoroutine);
                    ReturningCoroutine = StartCoroutine(Return());
                    yield break;
                }
            }
            else
            {
                ReturningCoroutine = StartCoroutine(Return());
                yield break;
            }
            yield return null;
        }
        ReturningCoroutine = StartCoroutine(Return());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float currentTime = 0;

            while (currentTime < ShootingInterval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            Projectile projectile = Instantiate(Projectile).GetComponent<Projectile>();
            projectile.transform.position = ProjectileSpawner.transform.position;
            projectile.SetTarget(Targets[0]);

            yield return null;
        }
    }

    private IEnumerator Return()
    {
        if (TargetingCoroutine != null)
        {
            StopCoroutine(TargetingCoroutine);
            TargetingCoroutine = null;
            StopCoroutine(ShootingCoroutine);
        }
        while (transform.rotation != DefaultRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, DefaultRotation, RotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void RemoveNulls()
    {
        foreach (GameObject x in Targets.ToList())
        {
            if (x == null)
            {
                Targets.Remove(x);
            }
        }
    }
}
