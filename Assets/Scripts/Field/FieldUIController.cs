using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FieldUIController : MonoBehaviour {

    public Text Scrap;

	// Use this for initialization
	void Start () {
		
	}
	
    public void ChangeScrap(int NewAmount)
    {
        Scrap.text = string.Format("Scrap: {0}", NewAmount.ToString());
    }
}
