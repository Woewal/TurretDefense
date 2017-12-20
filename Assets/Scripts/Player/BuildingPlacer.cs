using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class BuildingPlacer : MonoBehaviour
{
    Player player;
    public BuildingData buildingData;

    [SerializeField] Building buildingPrefab;
    [SerializeField] BuildingKit buildingKitPrefab;
    [SerializeField] BuildingIndicator indicator;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void InitiateBuilding(BuildingData data = null)
    {
        if (data != null)
        {
            buildingData = data;
        }
        else {
            buildingData = new BuildingData();
        }
        
        player.SetPrimaryAction(Place);
        player.SetSecondaryAction(Reset);
        player.SetTertaryAction(Build);
        indicator.gameObject.SetActive(true);
        indicator.DisplayBuildings(buildingData);

        player.state = Player.PlayerState.Building;
    }

    public void Reset()
    {
        buildingData = null;
        indicator.gameObject.SetActive(false);
        indicator.Reset();
        player.SetPrimaryAction(player.Interact);
        player.SetSecondaryAction(player.EmptyAction);
        player.SetTertaryAction(player.EmptyAction);

        player.state = Player.PlayerState.Default;
    }

    public void AddComponent(BuildingComponent component)
    {
        if (buildingData == null)
        {
            InitiateBuilding();
        }

        if (buildingData.components.Count <= 3)
        {
            buildingData.components.Add(component);
            indicator.DisplayBuildings(buildingData);
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
            if (indicator.CanPlace)
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
    }

    public void Build()
    {
        if (indicator.CanPlace)
        {
            Building building = Instantiate(buildingPrefab);
            building.transform.position = indicator.transform.position;
            building.transform.rotation = indicator.transform.rotation;

            foreach(BuildingComponent component in buildingData.components)
            {
                building.AddComponent(component);
            }

            Reset();
        }
    }

}

