using UnityEngine;
using System.Collections;
using Game.Building;

public class BuildingKit : Interactable
{

    public BuildingData data;
    public Material kitMaterial;

    public override void Interact(Player player)
    {
        //player.buildingPlacer.InitiateBuilding(data);
        player.interactables.Remove(this);
        Destroy(this.gameObject);
    }

    public override void BuildingInteract(Player player)
    {
        if (player.buildingPlacer.buildingData.components.Count + data.components.Count <= Building.maxComponents)
        {
            for (int i = 0; i < player.buildingPlacer.buildingData.components.Count; i++)
            {
                GameObject newComponent = Instantiate(player.buildingPlacer.buildingData.components[i].mesh, transform);

                Vector3 height = (data.components.Count - 1 + i + 1) * BuildingComponent.size * Vector3.up + Vector3.up * Building.baseHeight;

                newComponent.transform.position = new Vector3(transform.position.x, height.y, transform.position.z);

                data.components.Add(player.buildingPlacer.buildingData.components[i]);
                foreach(Renderer renderer in newComponent.GetComponentsInChildren<Renderer>())
                {
                    renderer.material = kitMaterial;
                }
            }

            player.buildingPlacer.Reset();
        }
    }
}
