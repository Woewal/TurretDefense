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

    float currentTime = 0;
    float currentInterval;

    Quaternion originalRotation;

    Coroutine shootingCoroutine;

    private void Start()
    {
        originalRotation = transform.rotation;

        targets = building.targetedEnemies;

        currentInterval = Mathf.Lerp(lowEnergyInterval, highEnergyInterval, building.energy.CurrentEnergyLerp);
    }

    private void Update()
    {
        if (building.targetedEnemies.Count > 0)
        {
            FollowTarget();
            Shoot();
        }
        else
        {
            StopFollow();
        }
    }

    public void StopFollow()
    {

    }

    public void FollowTarget()
    {

        if (targets[0] == null)
        {
            building.UpdateTargets();
            return;
        }
        Vector3 relativePos = (targets[0].transform.position + Vector3.up * .5f) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }



    private void Shoot()
    {
        if (currentTime > currentInterval)
        {
            Fire();
            currentInterval = Mathf.Lerp(lowEnergyInterval, highEnergyInterval, building.energy.CurrentEnergyLerp);
            currentTime = 0;
            return;
        }

        currentTime += Time.deltaTime;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        ChangeEnergy(-3f);
        bullet.transform.position = bulletEmitter.transform.position;
        bullet.transform.rotation = bulletEmitter.transform.rotation;
    }
}
