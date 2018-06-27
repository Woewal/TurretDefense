using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    [HideInInspector] public static GlobalController instance;
    [HideInInspector] public LevelController levelController;

    public GameSettings gameSettings;

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
        
        levelController = GetComponentInChildren<LevelController>();
    }
    
    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Debug"))
        {

        }
	}
}
