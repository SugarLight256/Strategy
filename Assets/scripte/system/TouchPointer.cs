using UnityEngine;
using System.Collections;

public class TouchPointer : MonoBehaviour {

    public Camera cam;
    public Camera_Pinch pinch;
	// Use this for initialization
	void Start () {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        pinch = GameObject.Find("MainCamera").GetComponent<Camera_Pinch>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount > 0)
        {
            transform.position =cam.ScreenToWorldPoint(Input.touches[0].position);
        }
	}

    void OnTriggerStay2D(Collider2D c)
    {
        pinch.SelectedObj = c.gameObject;
    }

}
