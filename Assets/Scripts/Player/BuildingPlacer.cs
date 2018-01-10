using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Game.Building;

public class BuildingPlacer : MonoBehaviour
{
    Player player;
    public BuildingData buildingData;
    public Building building;

    [SerializeField] Building buildingPrefab;
    [SerializeField] BuildingKit buildingKitPrefab;
    [SerializeField] BuildingIndicator indicator;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void InitiateBuilding(Building newBuilding = null)
    {
        if (newBuilding != null)
        {
            building = Instantiate(newBuilding, indicator.transform);
            building.transform.position = indicator.transform.position;
            building.transform.rotation = indicator.transform.rotation;
        }
        else
        {
            //indicator.DisplayBuildings(buildingData);
            building = Instantiate(buildingPrefab, indicator.transform);
        }

        player.SetPrimaryAction(Build);
        player.SetSecondaryAction(DestroyCurrentBuilding);
        indicator.gameObject.SetActive(true);

        building.assignedPlayer = player;

        

        player.state = Player.PlayerState.Building;
    }

    public void PickUpBuilding(Building newBuilding)
    {
        building = newBuilding;
        building.transform.SetParent(indicator.transform);
        building.transform.position = indicator.transform.position;
        building.transform.rotation = indicator.transform.rotation;

        player.SetPrimaryAction(Build);
        player.SetSecondaryAction(DestroyCurrentBuilding);
        indicator.gameObject.SetActive(true);

        building.assignedPlayer = player;

        building.SetColliding(false);

        player.state = Player.PlayerState.Building;
    }

    public void AddComponent(BuildingComponent component)
    {
        if (building == null)
        {
            InitiateBuilding();
        }

        if (building.components.Count <= 3)
        {
            //buildingData.components.Add(component);
            building.AddComponent(component);
            //indicator.DisplayBuildings(buildingData);
        }
        else
        {
            Debug.LogError("Can't put more");
        }
    }

    public void Build()
    {

        /*if (player.interactables.OfType<Building>().Any())
        {
            Building newBuilding = player.interactables.OfType<Building>().First();
            newBuilding.AddComponents(building.components);

            DestroyCurrentBuilding();
        }
        else*/ if (indicator.CanPlace)
        {
            building.transform.parent = player.transform.parent;
            building.SetColliding(true);
            building.assignedPlayer = null;
            player.interactables.Add(building);

            ResetReferences();
        }
    }

    public void DestroyCurrentBuilding()
    {
        Destroy(building.gameObject);
        ResetReferences();
    }

    public void ResetReferences()
    {
        building = null;
        indicator.gameObject.SetActive(false);
        indicator.Reset();
        player.SetPrimaryAction(player.Interact);
        player.SetSecondaryAction(player.EmptyAction);
        player.SetTertaryAction(player.EmptyAction);

        player.state = Player.PlayerState.Default;
    }

}

