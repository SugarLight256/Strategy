using UnityEngine;
using System.Collections;

public class Bull_CN : MonoBehaviour {

    public Bull_Main main;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    private void move()
    {



    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag != transform.tag)
        {
            c.gameObject.SendMessage("HPCalc", main.atk);
            Destroy(transform.gameObject);
        }
    }
}
