using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour
{
    public float CurrentEnergy
    {
        get
        {
            return Mathf.Clamp(currentEnergy / maxEnergy, 0, 1);
        }
    }
    
    [Header("Stats")]
    public float currentEnergy;
    public float maxEnergy = 200;



    [Header("Instantiating behaviour")]
    [SerializeField] EnergyBar energyBarPrefab = null;
    [SerializeField] Transform energyBarParent = null;

    private EnergyBar energyBar;

    virtual public void Start()
    {
        energyBar = Instantiate(energyBarPrefab, energyBarParent);
        energyBar.transform.SetParent(energyBarParent);
        currentEnergy = maxEnergy;
    }

    public void ChangeEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        energyBar.SetHealth(currentEnergy / maxEnergy);
    }
}
