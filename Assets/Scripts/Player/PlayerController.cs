using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rgbd;
    public float Speed;
    public float RotationSpeed;
    Player Player;
    BuildingPlacer BuildingPlacer;

    public Animator Animator;

    // Use this for initialization
    void Start()
    {
        Player = GetComponent<Player>();
        rgbd = GetComponent<Rigidbody>();
        BuildingPlacer = GetComponent<BuildingPlacer>();

        Player.SetPrimaryAction(Player.Interact);
        Player.SetSecondaryAction(Player.EmptyAction);
        Player.SetTertaryAction(BuildingPlacer.InitiateBuilding);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Animator.SetBool("Running", true);
        }
        else
        {
            Animator.SetBool("Running", false);
        }

        if (Input.GetButtonDown("PrimaryAction"))
        {
            Player.PrimaryAction();
        }

        if (Input.GetButtonDown("SecondaryAction"))
        {
            Player.SecondaryAction();
        }

        if (Input.GetButtonDown("TertaryAction"))
        {
            Player.TertaryAction();
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
