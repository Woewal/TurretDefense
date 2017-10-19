using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInformation : MonoBehaviour {

    public Text ComponentParent;
    public Text ModuleParent;

    public void Initialize (BuildingData.BuildingComponentData component)
    {
        ComponentParent.text = component.ComponentName;
        ModuleParent.text = component.ModuleName;
    }
}
