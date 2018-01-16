using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    public List<Interactable> interactables = new List<Interactable>();
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        foreach (Interactable interactable in interactables)
        {
            Debug.DrawLine(player.transform.position, interactable.transform.position, Color.green, 0.1f);
        }
    }

    public void Interact()
    {
        if (interactables.Count != 0)
        {
            interactables = interactables.OrderBy(
                i => Vector3.Distance(this.transform.position, i.transform.position)
            ).ToList();

            interactables[0].Interact(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            if (interactable.assignedPlayer != player)
            {
                interactables.Add(interactable);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactables.Remove(interactable);
        }
    }
}
