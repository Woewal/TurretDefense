using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadAttachables : MonoBehaviour {

    public List<BuildingAttachable> AvailableComponents;
    public BuildingAttachable DefaultComponent;
    public BuildingAttachable DefaultProjectile;

    public AttachableButton ComponentButton;

    private void Start()
    {
        AvailableComponents = new List<BuildingAttachable>();
        LoadButtons();
    }

    private void LoadButtons()
    {
        AttachableButton componentButton = Instantiate(ComponentButton, gameObject.transform);
        componentButton.BuildingAttachable = DefaultComponent;

        componentButton = Instantiate(ComponentButton, gameObject.transform);
        componentButton.BuildingAttachable = DefaultComponent;

        foreach (BuildingComponent availableComponent in AvailableComponents)
        {
            componentButton = Instantiate(ComponentButton, gameObject.transform);
            componentButton.BuildingAttachable = availableComponent;
        }
    }
}
