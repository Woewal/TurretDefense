using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class StoreController : MonoBehaviour {

    private List<BuildingData> AvailableBuildings;

    public EditableButton Button;

    public GameObject BuildingParent;
    public GameObject AttachmentParent;

    public void Initiate()
    {
        LoadAvailableAttachments();
        LoadAvailableBuildings();
        LoadButtons();
    }

    private void LoadAvailableAttachments()
    {
        GameController.instance.BuildingController.RetrieveComponent("Turret").Available = true;
        GameController.instance.BuildingController.RetrieveModule("Bullet").Available = true;

    }

    public void LoadAvailableBuildings()
    {
        if (SaveState.Instance.AvailableBuildings == null)
        {
            SaveState.Instance.AvailableBuildings = new List<BuildingData>();
            AvailableBuildings = SaveState.Instance.AvailableBuildings;
            BuildingData newBuildingData = new BuildingData();
            newBuildingData.AddComponentData("Turret", "Bullet");
            AvailableBuildings.Add(newBuildingData);
            SaveState.Save();
        }
        else {
            AvailableBuildings = SaveState.Instance.AvailableBuildings;
        }


    }

    public void LoadButtons()
    {
        foreach (Transform x in BuildingParent.transform)
        {
            Destroy(x.gameObject);
        }

        //foreach (BuildingAttachable availableComponent in AvailableAttachments)
        //{
            //Button button = Instantiate(Button, AttachmentParent.transform);
            //button.onClick.AddListener(delegate{ });
        //}

        foreach (BuildingData availableBuilding in AvailableBuildings)
        {
            EditableButton buildingButton = Instantiate(Button, BuildingParent.transform);
            buildingButton.Button.onClick.AddListener(delegate { BuildingEditorController.Instance.WorkhopController.LoadWorkshop(WorkshopController.EditMode.Edit, availableBuilding); });
            buildingButton.gameObject.AddComponent<BuildingButton>().Building = availableBuilding;
        }

        EditableButton newBuildingButton = Instantiate(Button, BuildingParent.transform);
        newBuildingButton.Button.onClick.AddListener(delegate { BuildingEditorController.Instance.WorkhopController.LoadWorkshop(WorkshopController.EditMode.New); });
        newBuildingButton.LoadText("New building");

        BuildingEditorController.SetSelected(BuildingParent.transform.GetChild(0).gameObject);

    }
}
