using UnityEngine;
using System.Collections;

public class TP_UI : MonoBehaviour {

    public GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = Input.touches[0].position;
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        Camera.GetComponent<Camera_Pinch>().SelectedObj = c.gameObject;
    }
}
