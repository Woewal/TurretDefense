using UnityEngine;
using Game.Building;

public class ComponentCrate : Interactable
{
    public BuildingComponent component;
    public Building buildingBasePrefab;

    [SerializeField] Vector3 offset;

    GameObject model;

    public void Start()
    {
        model = Instantiate(component.mesh, transform);
        model.transform.localPosition = offset;
        model.transform.Rotate(new Vector3(0, Random.Range(0, 359), 0));
        model.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }

    public void Update()
    {
        model.transform.Rotate(new Vector3(0, 50f, 0) * Time.deltaTime);
    }

    public override void Interact(Player player)
    {
        Building newBuilding = Instantiate(buildingBasePrefab);
        newBuilding.Interact(player);
        newBuilding.AddComponent(component);
    }
}