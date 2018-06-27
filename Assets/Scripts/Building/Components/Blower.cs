using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : BuildingComponent, ITargetable
{
    Quaternion originalRotation;

    float currentTime = 0;
    float interval = 8;

    [SerializeField] float blowCenterOffset;
    [SerializeField] Vector3 blowArea;

    Collider blowCollider;

    private void Start()
    {
        blowCollider = GetComponentInChildren<Collider>();
        originalRotation = transform.rotation;
    }

    public void OnTargetUpdate(Enemy enemy)
    {
        Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        currentTime += Time.deltaTime;

        AttemptShoot();
    }

    public void OnUntargetUpdate()
    {

    }

    void AttemptShoot()
    {
        if(currentTime > interval)
        {

            Shoot();

            currentTime = 0;
        }
    }

    void Shoot()
    {
        Collider[] colliders = Physics.OverlapBox(transform.forward * blowCenterOffset, blowArea, transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.forward * blowCenterOffset, blowArea);
    }
}
