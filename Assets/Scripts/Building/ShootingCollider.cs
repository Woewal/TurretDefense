using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCollider : MonoBehaviour
{
    public Turret Turret;
    public bool IsEnabled;

    private void OnTriggerEnter(Collider col)
    {
        if (IsEnabled)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Turret.Targets.Add(col.gameObject);
                Turret.CheckEnemies();
            }
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (IsEnabled)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Turret.Targets.Remove(col.gameObject);
                Turret.CheckEnemies();
            }
        }
    }
}
