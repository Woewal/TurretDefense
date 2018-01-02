using UnityEngine;
using System.Collections;

public class ComponentCrate : Interactable
{
    public BuildingComponent component; 

    public override void Interact(Player player)
    {
        player.buildingPlacer.AddComponent(component);
    }
}
