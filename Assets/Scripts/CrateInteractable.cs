using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class CrateInteractable : Interactable
{
    public Building StoredTurret;

    public override void Interact(Player player)
    {
        if (GameController.instance.Scrap >= StoredTurret.ScrapCost)
        {
            FieldController.instance.AddScrap(-StoredTurret.ScrapCost);
            player.FillPlayerHand(StoredTurret);
        }
    }
}
