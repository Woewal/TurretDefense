using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody rgbd;
    public float Speed;
    public float RotationSpeed;
    Player Player;

	// Use this for initialization
	void Start () {
        Player = GetComponent<Player>();
        rgbd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if(Input.GetButtonDown("Interact"))
        {
            if (Player.PlayerState == PlayerStates.Default)
            {
                Player.Interact();
            }
            else if(Player.PlayerState == PlayerStates.Carrying)
            {
                Player.PlaceBuilding();
            }
            
        }
	}

    void MovePlayer(float horizontalSpeed, float verticalSpeed)
    {
        rgbd.MovePosition(new Vector3(transform.position.x + horizontalSpeed * Speed, 0, transform.position.z + verticalSpeed * Speed));

        //Quaternion Rotation = Quaternion.LookRotation(transform.position - new Vector3(horizontalSpeed, 0, verticalSpeed));

        Quaternion Rotation = Quaternion.LookRotation(new Vector3(horizontalSpeed * 3, 0, verticalSpeed * 3));

        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * RotationSpeed);

        //transform.rotation = Quaternion.LookRotation(new Vector3(horizontalSpeed * 3, 0, verticalSpeed * 3));
    }
}
