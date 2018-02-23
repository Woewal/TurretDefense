using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    List<Rigidbody> collidingObjects = new List<Rigidbody>();

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            collidingObjects.Add(rb);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            collidingObjects.Remove(rb);
        }
    }

    private void Update()
    {
        if (collidingObjects.Count == 0)
            return;

        float conveyorVelocity = speed * Time.deltaTime;
        foreach (Rigidbody rb in collidingObjects)
        {
            rb.velocity = conveyorVelocity * transform.forward;
        }
    }
}
