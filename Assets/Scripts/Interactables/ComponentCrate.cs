using UnityEngine;
using System.Collections;

public class ComponentCrate : Interactable
{
    public BuildingComponent component; 

    public override void Interact(Player player)
    {
        if (GlobalController.instance.levelController.scrap - component.cost >= 0)
        {
            player.buildingPlacer.AddComponent(component);
            GlobalController.instance.levelController.AddScrap(-component.cost);
        }
        else
        {
            Debug.LogError("Player has not enough scrap");
        }
        
    }
}
