using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EditableButton : MonoBehaviour
{
    public Text Text;
    public Transform ButtonContentParent;
    public Image ButtonBackground;
    public GameObject DisabledLayer;

    public Button Button;

    public Color ActiveColor;

    private void Start()
    {
        ButtonBackground.color = ActiveColor;
    }

    public void LoadText(string text)
    {
        RemoveContent();
        Text newText = Instantiate(Text, ButtonContentParent);
        newText.text = text;
    }

    private void RemoveContent()
    {
        foreach (Transform x in ButtonContentParent.transform)
        {
            Destroy(x.gameObject);
        }
    }
}
