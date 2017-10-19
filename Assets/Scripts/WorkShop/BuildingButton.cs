using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, ISelectHandler {

    public BuildingData Building;

    public void OnSelect(BaseEventData eventData)
    {
        BuildingEditorController.Instance.LoadBuilding(Building);
    }
}
