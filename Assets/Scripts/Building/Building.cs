using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Building : Interactable {
    public List<BuildingComponent> components = new List<BuildingComponent>();

    public Particle DustEmitter;

    public bool On = true;

    public void BuildTurret(BuildingData data)
    {
        //Particle de = Instantiate(DustEmitter, this.transform);
        //de.transform.position = this.transform.position;

        for (int i = 0; i < data.components.Count; i++)
        {
            AddComponent(data.components[i]);
        }

        TurnOn();
    }

    public void AddComponent(BuildingComponent component)
    {
        if (components.Count < 4)
        {
            BuildingComponent newComponent = Instantiate(component, transform);
            newComponent.transform.Translate(components.Count * BuildingComponent.size * Vector3.up + Vector3.up * 0.8f);
            components.Add(newComponent);
        }
        else
        {
            Debug.LogError("Too many components");
        }
        
    }

    public void TurnOn()
    {
        foreach(BuildingComponent component in components)
        {
            if (component != null)
            {
                component.TurnOn();
            }
        }
    }

    public override void Interact(Player player)
    {
    }

    public override void BuildingInteract(Player player)
    {
        if(player.buildingPlacer.buildingData.components.Count + components.Count < 4)
        {
            foreach(BuildingComponent component in player.buildingPlacer.buildingData.components)
            {
                AddComponent(component);
            }

            player.buildingPlacer.Reset();
        }
    }
}
