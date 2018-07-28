using UnityEngine;
using System.Collections;
using Game.Building;

public abstract class BuildingComponent : MonoBehaviour
{
    public static float size = 0.6f;
    [HideInInspector] public Building building;

    internal float returnDuration = 0.5f;

    [Header("Properties")]
    public float rotationSpeed;
    public int cost;

    //0 == infinite;
    public int limitOfInstances;
    public GameObject mesh;

    public Coroutine currentCoroutine;
    
    /// <summary>
    /// Adds or subtracts energy
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeEnergy(float amount)
    {
        building.energy.ChangeEnergy(amount);
    }

    virtual public void Initialize() { }
}
