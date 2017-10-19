using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicator : MonoBehaviour {


    public GameObject building;
    private List<GameObject> Obstacles = new List<GameObject>();
    private Color enabledColor = new Color(0, 255, 0, 0.5f);
    private Color disabledColor = new Color(255, 0, 0, 0.5f);

    public bool CanPlace = true;

    private void OnEnable()
    {
        EnableBuilding();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            Obstacles.Add(other.gameObject);
            DisableBuilding();
            CanPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            Obstacles.Remove(other.gameObject);
            if (Obstacles.Count == 0)
            {
                EnableBuilding();
                CanPlace = true;
            }
        }
        
    }

    private void EnableBuilding()
    {
        foreach(Renderer renderer in building.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = enabledColor;
        }
    }

    private void DisableBuilding()
    {
        foreach (Renderer renderer in building.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = disabledColor;
        }
    }

    public void Reset()
    {
        building = null;
        Obstacles.Clear();
        gameObject.SetActive(false);
    }
}
