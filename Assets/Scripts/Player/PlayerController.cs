using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rgbd;
    public float Speed;
    public float RotationSpeed;
    Player player;

    public Animator Animator;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal" + player.playerNumber) != 0 || Input.GetAxis("Vertical" + player.playerNumber) != 0)
        {
            MovePlayer(Input.GetAxis("Horizontal" + player.playerNumber), Input.GetAxis("Vertical" + player.playerNumber));
            Animator.SetBool("Running", true);
        }
        else
        {
            Animator.SetBool("Running", false);
        }

        if (Input.GetButtonDown("PrimaryAction" + player.playerNumber))
        {
            player.PrimaryAction();
        }

        if (Input.GetButtonDown("SecondaryAction" + player.playerNumber))
        {
            player.SecondaryAction();
        }

        if (Input.GetButtonDown("TertaryAction" + player.playerNumber))
        {
            
            player.TertaryAction();
        }
    }

    void MovePlayer(float horizontalSpeed, float verticalSpeed)
    {
        rgbd.MovePosition(new Vector3(transform.position.x + horizontalSpeed * Speed, 0, transform.position.z + verticalSpeed * Speed));

        Quaternion Rotation = Quaternion.LookRotation(new Vector3(horizontalSpeed * 3, 0, verticalSpeed * 3));

        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * RotationSpeed);
    }
}
