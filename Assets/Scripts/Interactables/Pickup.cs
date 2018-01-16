using UnityEngine;
using System.Collections;

public abstract class Pickup : Interactable
{
    public abstract void Use();

    [HideInInspector] public Player player;

    public override void Interact(Player player)
    {
        this.player = player;
        transform.SetParent(player.indicator.transform);

        transform.position = player.indicator.transform.position;
        transform.rotation = player.indicator.transform.rotation;

        player.indicator.gameObject.SetActive(true);
        AssignPlayer(player);
        player.SetPrimaryAction(Use);

        DisableCollision();
    }

    public virtual void DisableCollision()
    {

    }

    public void Drop()
    {
        if(player.indicator.CanPlace)
        {
            transform.SetParent(null);
            DisassignPlayer();
            player.SetPrimaryAction(player.interactionController.Interact);
        }
    }

    public void Reset()
    {
        player.ResetPlayerActions();
    }
}
