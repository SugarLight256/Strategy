using UnityEngine;
using System.Collections;

public class TP_UI : MonoBehaviour {

    public Camera_Pinch cam;
    // Update is called once per frame
    void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera_Pinch>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = Input.touches[0].position;
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        cam.SelectedObj = c.gameObject;
    }
}
