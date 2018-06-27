using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rgbd;

    private float speed;
    [SerializeField] private float speedOrigin = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    Player player;

    public Animator animator;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(Input.GetAxis("Horizontal" + player.playerNumber), Input.GetAxis("Vertical" + player.playerNumber));

        if (Input.GetButtonDown("PrimaryAction" + player.playerNumber))
        {
            player.PrimaryAction();
            player.interactionController.holdsInteraction = true;
        }
        else
        {
            player.interactionController.holdsInteraction = false;
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

    private void MovePlayer(float h, float v)
    {
        if (h != 0f || v != 0f) // If horizontal or vertical are pressed then continue
        {
            speed = speedOrigin; // Modify the speed to adjust for moving on an angle

            Vector3 targetDirection = new Vector3(h, 0f, v); // Set a direction using Vector3 based on horizontal and vertical input
            rgbd.MovePosition(rgbd.position + targetDirection * speed * Time.deltaTime); // Move the players position based on current location while adding the new targetDirection times speed
            RotatePlayer(targetDirection); // Call the rotate player function sending the targetDirection variable
            animator.SetBool("Running", true);
        }
        else    // If horizontal or vertical are not pressed then continue
        {
            animator.SetBool("Running", false);
        }
    }

    private void RotatePlayer(Vector3 dir)
    {
        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        rgbd.MoveRotation(rotation); // Rotate the player to look at the new targetDirection
    }
}
