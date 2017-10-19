using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BuildingPlacer : MonoBehaviour
{
    Player Player;

    BuildingData BuildingToBePlaced;

    public BuildingIndicator BuildingIndicator;

    public Material TransparentMaterial;

    private void Start()
    {
        Player = GetComponent<Player>();
    }

    public void InitiateBuilding()
    {
        if (HasEnoughFunds(FieldController.instance.SelectedBuilding))
        {
            FillPlayerHand();
        }
    }

    private bool HasEnoughFunds(BuildingData bd)
    {
        if(GameController.instance.Scrap >= bd.Cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FillPlayerHand()
    {
        BuildingToBePlaced = FieldController.instance.SelectedBuilding;
        Building buildingIndicator = Building.CreateBuilding(BuildingIndicator.transform, BuildingToBePlaced);
        ModifyBuildingToIndicator(buildingIndicator);
        Player.SetPrimaryAction(PlaceBuilding);
        Player.SetTertaryAction(AlreadyBuildingError);
    }

    private void ModifyBuildingToIndicator(Building buildingIndicator)
    {
        foreach (MeshRenderer renderer in buildingIndicator.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = TransparentMaterial;
        }
        foreach (Collider collider in buildingIndicator.GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }
        foreach (NavMeshObstacle obstacle in buildingIndicator.GetComponentsInChildren<NavMeshObstacle>())
        {
            Destroy(obstacle);
        }

        BuildingIndicator.GetComponent<BuildingIndicator>().building = buildingIndicator.gameObject;
        BuildingIndicator.gameObject.SetActive(true);
    }

    public void PlaceBuilding()
    {
        if (!BuildingIndicator.CanPlace && HasEnoughFunds(BuildingToBePlaced))
        {
            return;
        }

        FieldController.instance.AddScrap(-BuildingToBePlaced.Cost);

        BuildingIndicator.Reset();

        Building newBuilding = Building.CreateBuilding(FieldController.instance.PlayingField.transform, BuildingToBePlaced);
        newBuilding.transform.position = BuildingIndicator.transform.position;
        newBuilding.transform.rotation = BuildingIndicator.transform.rotation;
        newBuilding.Place();
        BuildingToBePlaced = null;
        Player.SetPrimaryAction(Player.Interact);
        
        foreach (Transform x in BuildingIndicator.transform)
        {
            Destroy(x.gameObject);
        }
        Player.SetTertaryAction(InitiateBuilding);
    }

    public void CancelBuilding()
    {
        BuildingIndicator.Reset();
        foreach (Transform x in BuildingIndicator.transform)
        {
            Destroy(x.gameObject);
        }
        Player.SetPrimaryAction(Player.Interact);
        Player.SetTertaryAction(InitiateBuilding);
    }


    void AlreadyBuildingError()
    {
        Debug.Log("You're already building!");
    }
}

