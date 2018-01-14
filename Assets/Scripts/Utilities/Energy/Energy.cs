using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour
{
    public float CurrentEnergy
    {
        get
        {
            return Mathf.Clamp(currentEnergy / maxEnergy, 0.1f, 1);
        }
    }
    
    [Header("Stats")]
    [HideInInspector]
    private float currentEnergy = 500;
    [HideInInspector] private float maxEnergy = 500;



    [Header("Instantiating behaviour")]
    [SerializeField] EnergyBar energyBarPrefab = null;
    [SerializeField] Transform energyBarParent = null;

    private EnergyBar energyBar;

    private void Start()
    {
        energyBar = Instantiate(energyBarPrefab, energyBarParent);
        energyBar.transform.SetParent(energyBarParent);
    }

    public void ChangeEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        energyBar.SetHealth(currentEnergy / maxEnergy);
    }
}
