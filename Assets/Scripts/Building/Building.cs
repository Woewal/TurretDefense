using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public List<BuildingComponent> Modules;

    public Particle DustEmitter;

    public int ScrapCost = 50;

    public bool On = true;

	// Use this for initialization
	void Start () {
        Modules = new List<BuildingComponent>();

        foreach (BuildingComponent x in GetComponentsInChildren<BuildingComponent>())
        {
            Modules.Add(x);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Place()
    {
        Particle de = Instantiate(DustEmitter, this.transform);
        de.transform.position = this.transform.position;
        de.PlayParticle();
    }
}
