using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    [HideInInspector] public static GlobalController instance;
    [HideInInspector] public BuildingController buildingController;
    [HideInInspector] public LevelController levelController;

    public GameSettings gameSettings;

	// Use this for initialization
	void Awake ()
    {
        SetInstance();
        levelController = GetComponent<LevelController>();

        Vector2 origin = new Vector2(-1, -1);
        Vector2 direction = new Vector2(3, 3);

        Debug.Log(direction - origin);
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

        buildingController = GetComponent<BuildingController>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Debug"))
        {

        }
	}
}
