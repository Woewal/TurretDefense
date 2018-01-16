using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{

    [HideInInspector] public Player assignedPlayer;

    public abstract void Interact(Player player);

    public void AssignPlayer(Player player)
    {
        assignedPlayer = player;
        player.interactionController.interactables.Remove(this);
    }

    public void DisassignPlayer()
    {
        assignedPlayer = null;
    }
}
