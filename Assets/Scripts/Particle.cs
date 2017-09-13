using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public void PlayParticle()
    {
        foreach (ParticleSystem pe in GetComponentsInChildren<ParticleSystem>())
        {
            pe.Play();
        }
    }
}
