using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentEditor : MonoBehaviour {

    public GameObject ComponentParent;
    public GameObject ModuleParent;

    public Text TitleText;

    public EditableButton Button;

    EditableButton ComponentButton;
    EditableButton ModuleButton;

    BuildingComponent BuildingComponent;

    public int Level;

    public void Initiate(BuildingComponent component, int level)
    {
        Level = level;
        TitleText.text = string.Format("Component {0}", level + 1);
        ComponentButton = Instantiate(Button, ComponentParent.transform);
        ModuleButton = Instantiate(Button, ModuleParent.transform);

        if (component != null)
        {
            ComponentButton.LoadText(component.name);
            ModuleButton.LoadText(component.Module.name);
        }
        else
        {
            ComponentButton.LoadText("None");
            ModuleButton.LoadText("None");
        }

        if(level == 0)
        {
            BuildingEditorController.SetSelected(ComponentButton.gameObject);
        }

        ComponentButton.Button.onClick.AddListener(delegate { BuildingEditorController.Instance.WorkhopController.LoadAvailableComponents(this); });
        ModuleButton.Button.onClick.AddListener(delegate { BuildingEditorController.Instance.WorkhopController.LoadAvailableModules(this); });

        BuildingComponent = component;
    }

    public void ReplaceComponent(BuildingComponent component)
    {
        ComponentButton.LoadText(component.name);
        BuildingEditorController.SetSelected(ComponentButton.gameObject);
        BuildingComponent = BuildingEditorController.Instance.WorkhopController.EditedBuilding.AddComponent(component.name, Level);
    }

    public void DeleteComponent(int level)
    {
        BuildingEditorController.Instance.WorkhopController.EditedBuilding.RemoveComponent(level);
        ComponentButton.LoadText("None");
        BuildingEditorController.SetSelected(ComponentButton.gameObject);
        ModuleButton.LoadText("None");
    }

    public void ReplaceModule(BuildingModule module)
    {
        ModuleButton.LoadText(module.name);
        BuildingComponent.Module = module;
        BuildingEditorController.SetSelected(ModuleButton.gameObject);
    }
}
