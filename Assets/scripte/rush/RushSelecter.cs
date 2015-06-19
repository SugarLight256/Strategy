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
        if (transform.position.x + 70 <= Screen.width)
        {
            moveTrigger = false;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x -= 10;
            transform.position = pos;
        }
    }

    private void close()
    {
        if (transform.position.x - 75 >= Screen.width)
        {
            closeTrigger = false;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x += 10;
            transform.position = pos;
        }
    }

}
