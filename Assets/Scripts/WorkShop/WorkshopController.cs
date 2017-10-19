using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopController : MonoBehaviour
{
    BuildingEditorController BEC;

    public enum EditMode { Edit, New };
    [HideInInspector]
    public EditMode Mode;

    public BuildingData BuildingData;

    [HideInInspector]
    public Building EditedBuilding;

    public ComponentEditor ComponentEditor;
    public GameObject ComponentEditorParent;

    public GameObject AvailableComponentsParent;
    public GameObject AvailableModulesParent;

    public EditableButton EditableButton;

    private void Start()
    {
        BEC = BuildingEditorController.Instance;
    }

    public void LoadWorkshop(EditMode mode, BuildingData building = null)
    {
        EditedBuilding = BEC.LoadBuilding(building);
        BEC.LoadWorkshop();
        BuildingData = building;
        LoadComponents(EditedBuilding);
        Mode = mode;
    }

    public void Confirm()
    {
        if (Mode == EditMode.Edit)
        {
            EditedBuilding.ReplaceExistingBuilding(BuildingData);
        }
        else if (Mode == EditMode.New)
        {
            EditedBuilding.AddToAvailableBuildings();
        }

        BEC.LoadStore();
    }

    private void LoadComponents(Building building)
    {
        foreach (Transform x in ComponentEditorParent.transform)
        {
            Destroy(x.gameObject);
        }

        for (int i = building.Components.Count; i-- > 0;)
        {
            ComponentEditor newComponentEditor = Instantiate(ComponentEditor, ComponentEditorParent.transform);
            newComponentEditor.Initiate(building.Components[i], i);
        }
    }

    public void LoadAvailableComponents(ComponentEditor componentEditor)
    {
        AvailableModulesParent.SetActive(false);
        AvailableComponentsParent.SetActive(true);

        foreach (Transform x in AvailableComponentsParent.transform)
        {
            Destroy(x.gameObject);
        }

        foreach (BuildingComponent buildingComponent in GameController.instance.BuildingController.AvailableComponents.Values)
        {
            if (buildingComponent.Available)
            {
                EditableButton componentButton = Instantiate(EditableButton, AvailableComponentsParent.transform);
                componentButton.LoadText(buildingComponent.name);

                componentButton.Button.onClick.AddListener(delegate
                {
                    componentEditor.ReplaceComponent(buildingComponent);
                    UnloadAvailableComponents();
                });
            }

            EditableButton removeComponentButton = Instantiate(EditableButton, AvailableComponentsParent.transform);
            removeComponentButton.LoadText("Remove component");
            removeComponentButton.Button.onClick.AddListener(delegate
            {
                componentEditor.DeleteComponent(componentEditor.Level);
                UnloadAvailableComponents();
            });
        }

        BuildingEditorController.SetSelected(AvailableComponentsParent.transform.GetChild(0).gameObject);
    }

    private void UnloadAvailableComponents()
    {
        foreach (Transform x in AvailableComponentsParent.transform)
        {
            Destroy(x.gameObject);
        }
    }

    public void LoadAvailableModules(ComponentEditor componentEditor)
    {
        AvailableModulesParent.SetActive(true);
        AvailableComponentsParent.SetActive(false);

        foreach (Transform x in AvailableModulesParent.transform)
        {
            Destroy(x.gameObject);
        }

        foreach (BuildingModule buildingModule in GameController.instance.BuildingController.AvailableModules.Values)
        {
            if (buildingModule.Available)
            {
                EditableButton newButton = Instantiate(EditableButton, AvailableModulesParent.transform);
                newButton.LoadText(buildingModule.name);

                newButton.Button.onClick.AddListener(delegate
                {
                    componentEditor.ReplaceModule(buildingModule);
                    UnloadAvailableModules();
                });
            }
        }

        BuildingEditorController.SetSelected(AvailableModulesParent.transform.GetChild(0).gameObject);
    }

    private void UnloadAvailableModules()
    {
        foreach (Transform x in AvailableModulesParent.transform)
        {
            Destroy(x.gameObject);
        }
    }
}
