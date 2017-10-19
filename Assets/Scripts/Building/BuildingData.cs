using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuildingData
{
    public List<BuildingComponentData> Components = new List<BuildingComponentData>();
    public int Cost = 0;

    [System.Serializable]
    public struct BuildingComponentData
    {
        public BuildingComponentData(string componentName, string moduleName)
        {
            ComponentName = componentName;
            ModuleName = moduleName;
        }

        public string ComponentName;
        public string ModuleName;
    }

    public void AddComponentData(string componentName, string moduleName)
    {
        Components.Add(new BuildingComponentData(componentName, moduleName));
        Cost += GameController.instance.BuildingController.RetrieveComponent(componentName).ScrapCost;
        Debug.Log(Cost);
    }
}

