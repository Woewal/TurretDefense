using UnityEngine;
using System.Collections;

public class BuildingComponent : MonoBehaviour
{
    public string Name;
    public int BitCoinCost;
    public int ScrapCost;

    internal Building Building;

    internal void InitializeComponent()
    {
        Building = GetComponentInParent<Building>();
    }
}
