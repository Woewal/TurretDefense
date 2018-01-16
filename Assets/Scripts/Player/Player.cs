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

    public InteractionController interactionController;

    public Action PrimaryAction;
    public Action SecondaryAction;
    public Action TertaryAction;

    public PlacementIndicator indicator; 

    private void Start()
    {
        state = PlayerState.Default;
        controller = GetComponent<PlayerController>();
        indicator = GetComponentInChildren<PlacementIndicator>();
        interactionController = GetComponentInChildren<InteractionController>();
        ResetPlayerActions();

        GlobalController.instance.levelController.cameraController.targets.Add(transform);
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

    public void ResetPlayerActions()
    {
        SetPrimaryAction(interactionController.Interact);
    }

    public void EmptyAction()
    {

    }

}

