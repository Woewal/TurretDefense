using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : BuildingComponent {


    private void Start()
    {
        building.EnableTargeting += FollowTarget;
        building.DisableTargeting += StopFollow;
    }

    // Update is called once per frame
    void Update () {
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}

    public void FollowTarget()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        StartCoroutine(FollowTargetRoutine());

        
    }

    public void StopFollow()
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ReturnRotationRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            building.targetedEnemies.Add(other.gameObject.GetComponent<Enemy>());
            building.CheckTargets();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            building.targetedEnemies.Remove(other.gameObject.GetComponent<Enemy>());
            building.CheckTargets();
        }
    }
    
    public IEnumerator FollowTargetRoutine()
    {
        while (true)
        {
            if (building.targetedEnemies.Count > 0)
            {
                if (building.targetedEnemies[0] == null)
                {
                    building.CheckTargets();
                    yield return null;
                }
                Transform target = building.targetedEnemies[0].transform;

                Vector3 relativePos = (target.position) - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(relativePos);

                ChangeEnergy(-0.1f);

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

    public IEnumerator ReturnRotationRoutine()
    {
        float currentTime = 0;
        Quaternion currentRotation = transform.localRotation;

        while (currentTime < returnDuration)
        {
            transform.localRotation = Quaternion.Slerp(currentRotation, Quaternion.identity, currentTime / returnDuration);
            ChangeEnergy(-0.1f);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}

