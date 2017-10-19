using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Building : MonoBehaviour {
    public List<BuildingComponent> Components = new List<BuildingComponent>();

    public Particle DustEmitter;

    public int ScrapCost
    {
        get
        {
            int cost = 0;
            foreach(BuildingComponent bc in Components)
            {
                if (bc != null)
                {
                    Debug.Log("Scrapcost = " + bc.ScrapCost);
                    cost += bc.ScrapCost;
                }
            }
            return cost;
        }
    }

    public bool On = true;

    public void Place()
    {
        Particle de = Instantiate(DustEmitter, this.transform);
        de.transform.position = this.transform.position;
        de.PlayParticle();

        TurnOn();
    }

    public BuildingComponent AddComponent(string componentName, int level = 0, string moduleName = null)
    {

        if (Components[level] != null)
        {
            Destroy(Components[level].gameObject);
            Components[level] = null;
        }

        BuildingComponent newComponent = (BuildingComponent)Instantiate(GameController.instance.BuildingController.RetrieveComponent(componentName), transform);
        newComponent.name = GameController.instance.BuildingController.RetrieveComponent(componentName).name;
        newComponent.transform.Translate(new Vector3(0, level * BuildingComponent.Size / 2 + 0.75f));

        if (moduleName != null)
        {
            newComponent.Module = GameController.instance.BuildingController.RetrieveModule(moduleName);
        }

        while(Components[level] != null)
        {
            level++;
        }

        Components[level] = newComponent;

        return newComponent;
    }

    public void RemoveComponent(int level)
    {
        Destroy(Components[level].gameObject);
    }

    public void AddToAvailableBuildings()
    {
        BuildingData bd = new BuildingData();

        foreach(BuildingComponent bc in Components)
        {
            if (bc != null)
            {
                bd.AddComponentData(bc.name, bc.Module.name);
            }
        }
        bd.Cost = ScrapCost;
        SaveState.Instance.AvailableBuildings.Add(bd);
        SaveState.Save();
    }

    public void ReplaceExistingBuilding(BuildingData existingBuilding)
    {
        BuildingData bd = new BuildingData();

        foreach (BuildingComponent bc in Components)
        {
            if(bc != null)
            {
                bd.AddComponentData(bc.name, bc.Module.name);
            }
            bd.Cost = ScrapCost;
        }

        var index = SaveState.Instance.AvailableBuildings.FindIndex(a => a == existingBuilding);
        SaveState.Instance.AvailableBuildings.RemoveAt(index);
        SaveState.Instance.AvailableBuildings.Insert(index, bd);

        SaveState.Save();
    }

    public static Building CreateBuilding(Transform parent, BuildingData buildingData = null)
    {
        Building newBuilding = Instantiate(GameController.instance.BuildingController.EmptyBuilding, parent);

        newBuilding.InitiateComponents();

        if (buildingData != null)
        {
            for (int i = 0; i < buildingData.Components.Count; i++)
            {
                newBuilding.AddComponent(buildingData.Components[i].ComponentName, i, buildingData.Components[i].ModuleName);
            }
        }

        return newBuilding;
    }

    private void InitiateComponents()
    {
        if (Components.Count == 0)
        {

            for (int i = 0; i < 3; i++)
            {
                Components.Add(null);
            }
        }
    }

    public void TurnOn()
    {
        foreach(BuildingComponent component in Components)
        {
            if (component != null)
            {
                component.TurnOn();
            }
        }
    }
}
