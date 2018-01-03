using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent
{
    private List<Enemy> targets;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float returnDuration = 2f;

    [SerializeField] GameObject bulletEmitter;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireInterval = 1f;

    Quaternion originalRotation;

    private Coroutine targetingCoroutine;
    private Coroutine resettingCoroutine;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        originalRotation = transform.rotation;

        building.EnableTargeting += Target;
        building.DisableTargeting += StopTarget;

        targets = building.targetedEnemies;
    }

    private void Update()
    {
        if(targets.Count > 0)
        {
            if(targets[0] != null)
            {
                Debug.DrawLine(bulletEmitter.transform.position, targets[0].transform.position, Color.black, Time.deltaTime);
            }
        }
    }

    private void Target()
    {
        StopCoroutine(ResetRotation());
        targetingCoroutine = StartCoroutine(TargetEnemy());
        shootingCoroutine = StartCoroutine(Shoot());
    }

    private void StopTarget()
    {
        if(targetingCoroutine != null)
            StopCoroutine(TargetEnemy());
        resettingCoroutine = StartCoroutine(ResetRotation());
        if (shootingCoroutine != null)
            StopCoroutine(shootingCoroutine);
    }

    private IEnumerator TargetEnemy()
    {
        while(targets.Count > 0)
        {
            while (targets.Count > 0 && targets[0] != null)
            {
                Vector3 relativePos = (targets[0].transform.position + Vector3.up * .5f) - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(relativePos);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                yield return null;
            }
            if(targets.Count > 0)
            {
                if (targets[0] == null)
                {
                    targets.Remove(targets[0]);
                    building.CheckTargets();
                }
            }
            yield return null;
        }
        StopTarget();
    }

    private IEnumerator ResetRotation()
    {
        float currentTime = 0;

        while (currentTime < returnDuration)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, currentTime / returnDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = originalRotation;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float currentTime = 0;
            while (currentTime < fireInterval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletEmitter.transform.position;
        bullet.transform.rotation = bulletEmitter.transform.rotation;
    }
}
