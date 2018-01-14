using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{

    [HideInInspector] public Player assignedPlayer;

    public virtual void Interact(Player player)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (assignedPlayer == null)
            {
                other.gameObject.GetComponent<Player>().interactables.Add(this);
            }
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
