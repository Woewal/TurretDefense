using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField] float cameraSpeed = 1f;
    public List<Transform> targets = new List<Transform>();

    [SerializeField] Vector3 offset = Vector3.zero;

    private void Start()
    {
        GlobalController.instance.levelController.cameraController = this;
    }

    /// <summary>
    /// Makes the camera follow the player(s)
    /// </summary>
    private void LateUpdate()
    {
        if(targets.Count == 1)
        {
            FollowTarget();
        }
        else if(targets.Count > 1)
        {
            FollowMultipleTargets();
        }
    }

    /// <summary>
    /// Lerp to the player position
    /// </summary>
    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targets[0].position + offset, cameraSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Lerp to multiple targets
    /// </summary>
    private void FollowMultipleTargets()
    {
        Vector3 average = Vector3.zero;

        foreach (Transform transform in targets)
        {
            average += transform.position;
        }

        average /= targets.Count;

        transform.position = Vector3.Lerp(transform.position, average + offset, cameraSpeed * Time.deltaTime);
    }

}
