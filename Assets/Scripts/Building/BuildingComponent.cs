using UnityEngine;
using System.Collections;

public abstract class BuildingComponent : MonoBehaviour
{
    public static float size = 0.55f;

    internal Building Building;

    public GameObject mesh;

    public void TurnOn()
    {
        Building = GetComponentInParent<Building>();
        StartComponent();
    }

    public abstract void StartComponent();
}
