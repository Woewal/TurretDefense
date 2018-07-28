using UnityEngine;
using System.Collections;
using System;

public class Bullet : Projectile
{
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    /// <summary>
    /// Subtracts health from enemy when hit
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.health.Damage(damage);
            DestroyProjectile();
        }
    }
}
