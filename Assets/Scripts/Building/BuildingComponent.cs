using UnityEngine;
using System.Collections;

public abstract class BuildingComponent : BuildingAttachable
{
    public static float Size = 1.2f;

    internal Building Building;

    public BuildingAttachable Module;

    public void TurnOn()
    {
        Building = GetComponentInParent<Building>();
        StartComponent();
    }

    public abstract void StartComponent();
}
