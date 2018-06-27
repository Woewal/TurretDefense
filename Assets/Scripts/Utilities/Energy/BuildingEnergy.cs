using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEnergy : Energy {

    [SerializeField] GameObject buildingBase;
    Material energyIndicator;

    public override void Start()
    {
        base.Start();
        energyIndicator = buildingBase.GetComponent<Renderer>().materials[0];
    }

    private void Update()
    {

        energyIndicator.SetFloat("_EnergyLeft", 1);
    }

}
