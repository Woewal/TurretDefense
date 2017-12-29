using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField] float cameraSpeed = 1f;
    public List<Transform> targets = new List<Transform>();

    [SerializeField] Vector3 offset = Vector3.zero;

    private void LateUpdate()
    {
        if(targets.Count == 1)
        {
            FollowTarget();
        }
        else if(targets.Count > 1)
        {
            Debug.LogError("Not able to target multiple transforms (Yet to be implemented)");
        }
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targets[0].position + offset, cameraSpeed * Time.deltaTime);
    }

}
