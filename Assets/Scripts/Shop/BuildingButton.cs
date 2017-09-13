using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, ISelectHandler {

    public Building Building;

    public void Test()
    {
        Debug.Log("MYES");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selected");
    }
}
