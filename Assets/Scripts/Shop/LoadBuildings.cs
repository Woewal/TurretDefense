using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadBuildings : MonoBehaviour {

    public List<Building> AvailableBuildings;
    public Building DefaultBuilding;

    public BuildingButton TurretButton;

    private void Start()
    {
        AvailableBuildings = new List<Building>();
        LoadButtons();
    }

    private void LoadButtons()
    {
        BuildingButton turretButton = Instantiate(TurretButton, gameObject.transform);
        turretButton.Building = DefaultBuilding;

        foreach (Building availableBuilding in AvailableBuildings)
        {
            turretButton = Instantiate(TurretButton, gameObject.transform);
            turretButton.Building = availableBuilding;
        }
    }
}
