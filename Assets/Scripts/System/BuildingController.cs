using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public BuildingAttachable[] Components;
    public BuildingAttachable[] Modules;

    public Building EmptyBuilding;

    public Dictionary<string, BuildingComponent> AvailableComponents = new Dictionary<string, BuildingComponent>();
    public Dictionary<string, BuildingModule> AvailableModules = new Dictionary<string, BuildingModule>();

    private void Awake()
    {
        foreach (BuildingComponent component in Components)
        {
            AvailableComponents.Add(component.name, component);
        }

        foreach (BuildingModule module in Modules)
        {
            AvailableModules.Add(module.name, module);
        }
    }

    public BuildingComponent RetrieveComponent(string component)
    {
        return AvailableComponents[component];
    }

    public BuildingModule RetrieveModule(string module)
    {
        return AvailableModules[module];
    }
}



