using UnityEngine;
using System.Collections;

public class Compactor : BuildingComponent
{
    float energyLeft;

    [SerializeField] float consumptionRate = 20f;

    //TODO: change this code

    private void Update()
    {
        if (energyLeft > 0 && building.energy.currentEnergy < building.energy.maxEnergy)
        {
            float consumption = Time.deltaTime * consumptionRate;

            if (energyLeft - consumption <= 0)
            {
                energyLeft = 0;
            }
            energyLeft -= consumption;

            building.energy.ChangeEnergy(consumption);
        }
    }
   
    public void AddEnergy(float amount)
    {
        //energyLeft = Mathf.Clamp(energyLeft + amount, 0, 999999);
    }
}
