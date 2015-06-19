using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public int cool;
	private int Count;
	public GameObject[] unit;

	// Use this for initialization
	void Start () {
		Count = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (cool <= Count) {
			GameObject.Instantiate (unit[0], transform.position, transform.rotation);
			Count=0;
		}
		Count++;

	}
}
