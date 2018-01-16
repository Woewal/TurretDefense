using UnityEngine;
using System.Collections;
using System.Linq;
using Game.Building;
using System.Collections.Generic;

public class Battery : Pickup
{
    [SerializeField] float energy;

    public override void Use()
    {
        //Check if can use
        if (player.interactionController.interactables.OfType<Building>().Any())
        {
            Building building = player.interactionController.interactables.OfType<Building>().First();
            
            if (building.components.OfType<Compactor>().Any())
            {
                Compactor compactor = building.components.OfType<Compactor>().First();
                compactor.AddEnergy(energy);

                Destroy(this.gameObject);
                Reset();
            }
        }
        else
        {
            Drop();
        }
    }
}
