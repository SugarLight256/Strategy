using UnityEngine;
using System.Collections;

public class Particle_Destroyer : MonoBehaviour {

    public ParticleSystem myParticleSystem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (myParticleSystem != null && myParticleSystem.IsAlive(true) == false)
        {
            Destroy(transform.gameObject);
        }
	}
}
