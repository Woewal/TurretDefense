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
        LoadAvailableBuildings();

        LoadButtons();
    }

    private void LoadAvailableBuildings()
    {
        AvailableBuildings = SaveState.Instance.AvailableBuildings;

        if (AvailableBuildings == null)
        {
            AvailableBuildings = new List<Building>();
            AvailableBuildings.Add(DefaultBuilding);
            SaveState.Save();
        }
    }

    private void LoadButtons()
    {
        foreach (Building availableBuilding in AvailableBuildings)
        {
            BuildingButton buildingButton = Instantiate(TurretButton, gameObject.transform);
            buildingButton.Building = availableBuilding;
        }
    }
}
