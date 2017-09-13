using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCollider : MonoBehaviour {
    private Turret Turret;

    public void Start()
    {
        Turret = gameObject.GetComponentInParent<Turret>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Turret.Targets.Add(col.gameObject);
            Turret.CheckTargets();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Turret.Targets.Remove(col.gameObject);
            Turret.CheckTargets();
        }
    }
}
