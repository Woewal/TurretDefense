using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent
{
    private List<Enemy> targets;

    [SerializeField] GameObject bulletEmitter;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float highEnergyInterval = 0.5f;
    [SerializeField] float lowEnergyInterval = 3;

    Quaternion originalRotation;

    Coroutine shootingCoroutine;

    private void Start()
    {
        originalRotation = transform.rotation;

        building.EnableTargeting += Target;
        building.DisableTargeting += StopFollow;

        targets = building.targetedEnemies;
    }

    private void Update()
    {
        if (targets.Count > 0)
        {
            if (targets[0] != null)
            {
                Debug.DrawLine(bulletEmitter.transform.position, targets[0].transform.position, Color.black, Time.deltaTime);
            }
        }
    }

    public void StopFollow()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        if(shootingCoroutine != null)
            StopCoroutine(shootingCoroutine);
        currentCoroutine = StartCoroutine(ReturnRotationRoutine());

    }

    private void Target()
    {
        currentCoroutine = StartCoroutine(FollowTargetRoutine());
        shootingCoroutine = StartCoroutine(Shoot());
    }

    public IEnumerator FollowTargetRoutine()
    {
        while (true)
        {
            if(targets.Count > 0)
            {
                if (targets[0] == null)
                {
                    building.CheckTargets();
                    yield return null;
                }
                Vector3 relativePos = (targets[0].transform.position + Vector3.up * .5f) - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(relativePos);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                yield return null;
            }
            else
            {
                building.CheckTargets();
                yield return null;
            }
        }
    }



    private IEnumerator Shoot()
    {
        while (true)
        {
            float interval = Mathf.Lerp(lowEnergyInterval, highEnergyInterval, building.energy.CurrentEnergy);

            float currentTime = 0;
            while (currentTime < interval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            Fire();
        }
    }

    public IEnumerator ReturnRotationRoutine()
    {
        float currentTime = 0;
        Quaternion currentRotation = transform.localRotation;

        while (currentTime < returnDuration)
        {
            transform.localRotation = Quaternion.Slerp(currentRotation, Quaternion.identity, currentTime / returnDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        ChangeEnergy(-1f);
        bullet.transform.position = bulletEmitter.transform.position;
        bullet.transform.rotation = bulletEmitter.transform.rotation;
    }
}
