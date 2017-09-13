using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public enum PlayerStates { Default, Carrying }

public class Player : MonoBehaviour
{
    public List<Interactable> Interactables;
    public PlayerStates PlayerState;
    public PlayerSpaceIndicator SpaceIndicator;

    public void Start()
    {
        Interactables = new List<Interactable>();
    }

    public void Interact()
    {
        if (Interactables.Count != 0)
        {
            Interactables = Interactables.OrderBy(
                x => Vector3.Distance(this.transform.position, x.transform.position)
            ).ToList();

            Interactables[0].Interact(this);
        }
    }

    Building BuildingToBePlaced;
    public void FillPlayerHand(Building building)
    {
        PlayerState = PlayerStates.Carrying;
        BuildingToBePlaced = building;
        SpaceIndicator.gameObject.SetActive(true);
    }

    public void PlaceBuilding()
    {
        if (SpaceIndicator.CanPlace)
        {
            Building building = Instantiate(BuildingToBePlaced);
            building.transform.position = SpaceIndicator.transform.position;
            building.transform.rotation = gameObject.transform.rotation;
            building.Place();
            BuildingToBePlaced = null;
            PlayerState = PlayerStates.Default;
            SpaceIndicator.gameObject.SetActive(false);
        }
    }
}
