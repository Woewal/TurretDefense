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
        player.SetSecondaryAction(Reset);
        indicator.gameObject.SetActive(true);

        building.GetComponent<Collider>().isTrigger = true;

        player.state = Player.PlayerState.Building;
    }

    public void Reset()
    {
        Destroy(building.gameObject);
        building = null;
        indicator.gameObject.SetActive(false);
        indicator.Reset();
        player.SetPrimaryAction(player.Interact);
        player.SetSecondaryAction(player.EmptyAction);
        player.SetTertaryAction(player.EmptyAction);

        player.state = Player.PlayerState.Default;
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

    public void Place()
    {
        if (player.interactables.OfType<Building>().Any() || player.interactables.OfType<BuildingKit>().Any())
        {
            player.Interact();
        }
        else
        {
            BuildingKit kit = Instantiate(buildingKitPrefab).GetComponent<BuildingKit>();
            kit.data = buildingData;
            kit.transform.position = indicator.transform.position;
            kit.transform.rotation = indicator.transform.rotation;

            GameObject game = Instantiate(indicator.indicatorObject, kit.transform);
            game.transform.rotation = kit.transform.rotation;

            Reset();
        }
    }

    public void Build()
    {
        if (player.interactables.OfType<Building>().Any())
        {
            Building newBuilding = player.interactables.OfType<Building>().First();
            newBuilding.AddComponents(building.components);

            Reset();
        }
        else if (indicator.CanPlace)
        {
            Building newBuilding = Instantiate(building);
            newBuilding.GetComponent<Collider>().isTrigger = false;
            newBuilding.transform.position = indicator.transform.position;
            newBuilding.transform.rotation = indicator.transform.rotation;
            //newBuilding.AddComponents(building.components);

            Reset();
        }
    }

}

