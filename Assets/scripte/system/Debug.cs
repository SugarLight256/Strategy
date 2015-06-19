using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {

    public GameObject deb;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(deb, transform.position, transform.rotation);
	}
}
