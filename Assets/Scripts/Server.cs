using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    Health health;

    private void Awake()
    {

        GlobalController.instance.levelController.server = this;
    }

    // Use this for initialization
    void Start () {
        health = GetComponent<Health>();
        health.ZeroHealth += GlobalController.instance.levelController.ExitLevel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
