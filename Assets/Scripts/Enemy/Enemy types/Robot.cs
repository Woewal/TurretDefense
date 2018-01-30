 using UnityEngine;
using System.Collections;

public class Robot : Enemy
{

    // Use this for initialization
    void Start()
    {
        Initiate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Attack()
    {
        target.Damage(damage);
    }
}
