using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BuildingEditorController : MonoBehaviour {
    public static BuildingEditorController Instance;

    [HideInInspector]
    public WorkshopController WorkhopController;
    [HideInInspector]
    public StoreController StoreController;
    
    public GameObject Workshop;
    public GameObject Store;

    public GameObject BuildingDestination;

    private enum StartFunction { LoadNextWave, ConfirmBuilding };
    private StartFunction StartAction = StartFunction.LoadNextWave;

    [HideInInspector]
    public Building DisplayedBuilding;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        WorkhopController = GetComponent<WorkshopController>();
        StoreController = GetComponent<StoreController>();

        LoadStore();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (StartAction == StartFunction.ConfirmBuilding)
            {
                WorkhopController.Confirm();
            }
            else if (StartAction == StartFunction.LoadNextWave)
            {
                SceneManager.LoadScene("Field");
            }
        }
    }

    public Building LoadBuilding (BuildingData building)
    {
        foreach (Transform x in BuildingDestination.transform)
        {
            Destroy(x.gameObject);
        }
        
        return Building.CreateBuilding(BuildingDestination.transform, building);
    }

    public void LoadWorkshop()
    {
        Workshop.SetActive(true);
        Store.SetActive(false);
        StartAction = StartFunction.ConfirmBuilding;

        SetSelected(StoreController.BuildingParent.transform.GetChild(0).gameObject);
    }

    public void LoadStore()
    {
        Store.SetActive(true);
        Workshop.SetActive(false);
        StartAction = StartFunction.LoadNextWave;
        StoreController.Initiate();
    }

    static public void SetSelected(GameObject gameObject)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
