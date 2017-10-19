using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationController : MonoBehaviour {

    private List<BuildingData> AvailableBuildings;

    private int BuildingSelectedIndex = 0;

    public ComponentInformation ComponentInformation;
    public GameObject ComponentInformationParent;

    public Text CostText;

    private void Start()
    {
        AvailableBuildings = SaveState.Instance.AvailableBuildings;
        LoadBuilding(0);
    }
    
    void Update () {
		if(Input.GetButtonDown("SelectLeftHorizontal"))
        {
            if(BuildingSelectedIndex == 0)
            {
                BuildingSelectedIndex = AvailableBuildings.Count - 1;
            }
            else
            {
                BuildingSelectedIndex--;
            }
            LoadBuilding(BuildingSelectedIndex);
        }
        else if(Input.GetButtonDown("SelectRightHorizontal"))
        {
            if(BuildingSelectedIndex == AvailableBuildings.Count - 1)
            {
                BuildingSelectedIndex = 0;
            }
            else
            {
                BuildingSelectedIndex++;
            }
            LoadBuilding(BuildingSelectedIndex);
        }
	}

    void LoadBuilding(int Index)
    {
        foreach (Transform x in ComponentInformationParent.transform)
        {
            Destroy(x.gameObject);
        }

        CostText.text = "Cost: " + AvailableBuildings[Index].Cost.ToString() + " copper";

        for (int i = 0; i < 3; i++)
        {
            if (i < AvailableBuildings[Index].Components.Count)
            {
                ComponentInformation newComponentInformation = Instantiate(ComponentInformation, ComponentInformationParent.transform);
                newComponentInformation.Initialize(AvailableBuildings[Index].Components[i]);
            }
            else
            {
                var go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(ComponentInformationParent.transform);
                go.transform.SetAsFirstSibling();
            }
            
        }

        FieldController.instance.SelectedBuilding = AvailableBuildings[Index];
    }
}
