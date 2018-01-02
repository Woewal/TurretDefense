using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    public float Speed;

    public float damage = 10f;

    internal void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
