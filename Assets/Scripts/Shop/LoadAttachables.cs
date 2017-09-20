using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadAttachables : MonoBehaviour {

    public List<BuildingAttachable> AvailableAttachments;
    public BuildingAttachable DefaultComponent;
    public BuildingAttachable DefaultProjectile;

    public AttachableButton ComponentButton;

    private void Start()
    {
        LoadAvailableAttachments();
        LoadButtons();
    }

    private void LoadAvailableAttachments()
    {
        AvailableAttachments = SaveState.Instance.AvailableAttachments;

        if (AvailableAttachments == null)
        {
            AvailableAttachments = new List<BuildingAttachable>();
            AvailableAttachments.Add(DefaultComponent);
            AvailableAttachments.Add(DefaultProjectile);
            SaveState.Save();
        }
    }

    private void LoadButtons()
    {
        foreach (BuildingAttachable availableComponent in AvailableAttachments)
        {
            AttachableButton componentButton = Instantiate(ComponentButton, gameObject.transform);
            componentButton.BuildingAttachable = availableComponent;
        }
    }
}
