using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicator : MonoBehaviour {

    private List<GameObject> obstacles = new List<GameObject>();

    [SerializeField] Material enabledMaterial;
    [SerializeField] Material disabledMaterial;

    public GameObject indicatorObject;
    List<GameObject> componentObjects = new List<GameObject>();

    BuildingData buildingData;

    List<GameObject> buildingPreviewComponents = new List<GameObject>();
    List<GameObject> kitPreviewComponents = new List<GameObject>();

    public bool CanPlace = true;
    private void OnEnable()
    {
        EnableBuilding();
        indicatorObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Building>())
        {
            indicatorObject.SetActive(false);
            DisplayComponentsOnBuilding(other.GetComponent<Building>());
        }
        else if (other.GetComponent<BuildingKit>())
        {
            indicatorObject.SetActive(false);
            DisplayComponentsOnKit(other.GetComponent<BuildingKit>());
        }
        else if (!other.isTrigger)
        {
            obstacles.Add(other.gameObject);
            DisableBuilding();
            CanPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Building>() || other.GetComponent<BuildingKit>())
        {
            indicatorObject.SetActive(true);
            ResetPreviewComponents();
        }
        else if (!other.isTrigger)
        {
            obstacles.Remove(other.gameObject);
            if (obstacles.Count == 0)
            {
                EnableBuilding();
                CanPlace = true;
            }
        }
        
    }

    private void EnableBuilding()
    {
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = enabledMaterial;
        }
    }

    private void DisableBuilding()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = disabledMaterial;
        }
    }

    public void DisplayBuildings(BuildingData data)
    {
        buildingData = data;

        foreach(GameObject obj in componentObjects)
        {
            Destroy(obj);
        }
        componentObjects = new List<GameObject>();
        for (int i = 0; i < data.components.Count; i++)
        {
            GameObject component = Instantiate(data.components[i].mesh, indicatorObject.transform);
            component.transform.Translate(i * BuildingComponent.size * Vector3.up + Vector3.up * 0.8f);
            componentObjects.Add(component);
        }
        if(obstacles.Count == 0)
        {
            EnableBuilding();
        }
        else
        {
            DisableBuilding();
        }
    }

    void DisplayComponentsOnBuilding(Building building)
    {
        if (building.components.Count + buildingData.components.Count > 3)
        {
            return;
        }

        for(int i = 0; i < buildingData.components.Count; i++)
        {
            GameObject newComponent = Instantiate(buildingData.components[i].mesh);

            Vector3 height = (building.components.Count - 1 + i + 1) * BuildingComponent.size * Vector3.up + Vector3.up * 0.8f;

            newComponent.transform.position = new Vector3(building.transform.position.x, height.y, building.transform.position.z);
            newComponent.transform.rotation = building.transform.rotation;

            buildingPreviewComponents.Add(newComponent);

            foreach(Renderer renderer in newComponent.GetComponentsInChildren<Renderer>())
            {
                renderer.material = enabledMaterial;
            }
        }
    }

    void DisplayComponentsOnKit(BuildingKit kit)
    {
        if (kit.data.components.Count + buildingData.components.Count > 3)
        {
            return;
        }

        for (int i = 0; i < buildingData.components.Count; i++)
        {
            GameObject newComponent = Instantiate(buildingData.components[i].mesh);

            Vector3 height = (kit.data.components.Count - 1 + i + 1) * BuildingComponent.size * Vector3.up + Vector3.up * 0.8f;

            newComponent.transform.position = new Vector3(kit.transform.position.x, height.y, kit.transform.position.z);
            newComponent.transform.rotation = kit.transform.rotation;

            kitPreviewComponents.Add(newComponent);

            foreach (Renderer renderer in newComponent.GetComponentsInChildren<Renderer>())
            {
                renderer.material = enabledMaterial;
            }
        }
    }

    void ResetPreviewComponents()
    {
        foreach(GameObject component in buildingPreviewComponents)
        {
            Destroy(component);
        }
        foreach (GameObject component in kitPreviewComponents)
        {
            Destroy(component);
        }
        buildingPreviewComponents = new List<GameObject>();
        kitPreviewComponents = new List<GameObject>();
    }

    public void Reset()
    {
        obstacles.Clear();
        ResetPreviewComponents();
        gameObject.SetActive(false);
    }
}
