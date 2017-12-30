using UnityEngine;
using System.Collections;
using Game.Building;

public abstract class BuildingComponent : MonoBehaviour
{
    public static float size = 0.55f;
    [HideInInspector] public Building building;

    //0 == infinite;
    public int limitOfInstances;
    public GameObject mesh;
}
