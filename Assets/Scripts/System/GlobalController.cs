using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    public static GlobalController instance;
    public BuildingController BuildingController;
    public int Scrap = 100;

	// Use this for initialization
	void Awake ()
    {
        SetInstance();
        
    }

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        BuildingController = GetComponent<BuildingController>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Debug"))
        {

        }
	}
}
