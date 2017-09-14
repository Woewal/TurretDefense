using UnityEngine;
using System.Collections;

public class BuildingComponent : BuildingAttachable
{
    internal Building Building;

    internal void InitializeComponent()
    {
        Building = GetComponentInParent<Building>();
    }
}
