using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    public List<Interactable> interactables = new List<Interactable>();
    Player player;

    public bool holdsInteraction = false;

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

    /// <summary>
    /// Interacts with the closest interactable
    /// </summary>
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

    /// <summary>
    /// Adds an interactable to the list when in range of the player
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Removes an interactable from the list when out of range from the player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactables.Remove(interactable);
        }
    }
}
