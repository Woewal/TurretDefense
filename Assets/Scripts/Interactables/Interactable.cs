﻿using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact(Player player)
    {

    }
    public virtual void BuildingInteract(Player player)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().interactables.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().interactables.Remove(this);
        }
    }
}