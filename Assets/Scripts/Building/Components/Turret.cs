using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent, ITargetable
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
        currentInterval = Mathf.Lerp(lowEnergyInterval, highEnergyInterval, building.energy.CurrentEnergy);
    }


    /// <summary>
    /// Points at the target and shoots if able to
    /// </summary>
    /// <param name="enemy"></param>
    public void OnTargetUpdate(Enemy enemy)
    {
        Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Debug.Log(Quaternion.Angle(transform.rotation, targetRotation));

        AttemptShoot();

        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// Return to orinigal rotation
    /// </summary>
    public void OnUntargetUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// Shoots if requirements are met
    /// </summary>
    private void AttemptShoot()
    {
        if (currentTime > currentInterval)
        {
            Fire();
            currentInterval = Mathf.Lerp(lowEnergyInterval, highEnergyInterval, building.energy.CurrentEnergy);
            currentTime = 0;
            return;
        }

        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// Spawns a bullet
    /// </summary>
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        ChangeEnergy(-3f);
        bullet.transform.position = bulletEmitter.transform.position;
        bullet.transform.rotation = bulletEmitter.transform.rotation;
    }
}
