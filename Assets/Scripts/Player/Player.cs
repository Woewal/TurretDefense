using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class Player : MonoBehaviour
{
    public List<Interactable> Interactables = new List<Interactable>();

    public Action PrimaryAction;
    public Action SecondaryAction;
    public Action TertaryAction;

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

