using UnityEngine;
using System.Collections;

public class RushSelecter : MonoBehaviour {

    public GameObject Rush_Manager;

    public bool moveTrigger  = false;
    public bool closeTrigger = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(moveTrigger && closeTrigger)
        {
            closeTrigger = true;
            moveTrigger  = false;
        }
        if (moveTrigger)
        {
            move();
        }
        if (closeTrigger)
        {
            close();
        }
	}

    private void move()
    {
        if (transform.position.y >= 75)
        {
            moveTrigger = false;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y += 10;
            transform.position = pos;
        }
    }

    private void close()
    {
        if (transform.position.y <= -85)
        {
            closeTrigger = false;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y -= 10;
            transform.position = pos;
        }
    }

}
