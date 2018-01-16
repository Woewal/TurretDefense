using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : BuildingComponent
{


    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (building.targetedEnemies.Count > 0)
        {
            FollowTarget();
        }
        else
        {
            StopFollow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!building.targetedEnemies.Contains(other.gameObject.GetComponent<Enemy>()))
                building.targetedEnemies.Add(other.gameObject.GetComponent<Enemy>());
            building.UpdateTargets();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            building.targetedEnemies.Remove(other.gameObject.GetComponent<Enemy>());
            building.UpdateTargets();
        }
    }

    public void FollowTarget()
    {
        if (building.targetedEnemies[0] == null)
        {
            building.UpdateTargets();
            return;
        }
        Transform target = building.targetedEnemies[0].transform;

        Vector3 relativePos = (target.position) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        ChangeEnergy(-0.1f);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void StopFollow()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 7);
    }

}

