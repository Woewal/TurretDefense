using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    GameObject Target;

    private void Start()
    {
        Target = Camera.main.gameObject;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);
    }
}