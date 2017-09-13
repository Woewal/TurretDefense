using UnityEngine;
using System.Collections;
using System;

public class Bullet : Projectile
{
    public override void Fire()
    {
        StartCoroutine(FireInStaticDirection());
    }
}
