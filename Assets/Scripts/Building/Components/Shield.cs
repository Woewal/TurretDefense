using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BuildingComponent {
    private void Update()
    {
        ChangeEnergy(-0.1f);
    }

    /// <summary>
    /// Destroys enemy projectiles whenever they hit the shield
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var projectile = other.GetComponent<Projectile>();

        if(projectile != null)
        {
            if (projectile.isEnemyProjectile == true)
            {
                projectile.DestroyProjectile();
                ChangeEnergy(-1f);
            }
        }
    }
}
