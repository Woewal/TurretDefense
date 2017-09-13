using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(Player player);

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Interactables.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Interactables.Remove(this);
        }
    }
}
