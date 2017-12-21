using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : BuildingComponent {
    float rotationSpeed = 30f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
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




}
