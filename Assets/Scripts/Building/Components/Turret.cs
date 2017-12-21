using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : BuildingComponent
{
    private List<Enemy> targets;
    float rotationSpeed = 3f;
    float returnDuration = 2f;

    Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;

        building.EnableTargeting += Target;
        building.DisableTargeting += StopTarget;

        targets = building.targetedEnemies;
    }

    private void Target()
    {
        StopCoroutine(ResetRotation());
        StartCoroutine(TargetEnemy());
    }

    private void StopTarget()
    {
        StopCoroutine(TargetEnemy());
        StartCoroutine(ResetRotation());
    }

    private void UpdateTarget()
    {

    }

    private IEnumerator TargetEnemy()
    {
        while (targets.Count > 0)
        {
            Vector3 relativePos = targets[0].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            yield return null;
        }
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
}
