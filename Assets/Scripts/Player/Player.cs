using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class Player : MonoBehaviour
{
    public int playerNumber;

    public enum PlayerState { Default, Building }

    public PlayerState state;

    [HideInInspector] public PlayerController controller;
    [HideInInspector] public BuildingPlacer buildingPlacer;

    public List<Interactable> interactables = new List<Interactable>();

    public Action PrimaryAction;
    public Action SecondaryAction;
    public Action TertaryAction;

    private void Start()
    {
        state = PlayerState.Default;
        controller = GetComponent<PlayerController>();
        buildingPlacer = GetComponent<BuildingPlacer>();
    }

    private void Update()
    {
        foreach(Interactable mlem in interactables)
        {
            Debug.DrawLine(transform.position, mlem.transform.position, Color.green, 0.1f);
        }
    }

    public void Interact()
    {
        if (interactables.Count != 0)
        {
            interactables = interactables.OrderBy(
                x => Vector3.Distance(this.transform.position, x.transform.position)
            ).ToList();

            if (state == PlayerState.Default)
            {
                interactables[0].Interact(this);
            }
        }
    }

    public void SetPrimaryAction(Action action)
    {
        PrimaryAction = action;
    }

    public void SetSecondaryAction(Action action)
    {
        SecondaryAction = action;
    }

    public void SetTertaryAction(Action action)
    {
        TertaryAction = action;
    }

    public void EmptyAction()
    {

    }

}

