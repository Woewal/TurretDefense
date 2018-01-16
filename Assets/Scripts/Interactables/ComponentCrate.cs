using UnityEngine;
using System.Collections;
using Game.Building;

public class ComponentCrate : Interactable
{
    public BuildingComponent component;
    public Building buildingBasePrefab;

    public override void Interact(Player player)
    {
        if (GlobalController.instance.levelController.scrap - component.cost >= 0)
        {
            Building newBuilding = Instantiate(buildingBasePrefab);
            newBuilding.Interact(player);
            newBuilding.AddComponent(component);
            
            //player.buildingPlacer.AddComponent(component);
            GlobalController.instance.levelController.AddScrap(-component.cost);
        }
        else
        {
            Debug.Log("Player has not enough scrap");
        }
        
    }
}
