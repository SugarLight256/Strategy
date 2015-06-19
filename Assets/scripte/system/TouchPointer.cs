using UnityEngine;
using System.Collections;

public class TouchPointer : MonoBehaviour {

    public GameObject Camera;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount > 0)
        {
            transform.position =Camera.GetComponent<Camera>().ScreenToWorldPoint(Input.touches[0].position);
        }
	}

    void OnTriggerStay2D(Collider2D c)
    {
        Camera.GetComponent<Camera_Pinch>().SelectedObj = c.gameObject;
    }

}
