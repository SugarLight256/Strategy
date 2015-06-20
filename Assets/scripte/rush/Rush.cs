﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rush : MonoBehaviour {

    public List<GameObject> Unit;

    public GameObject MainCamera;

    private Vector2 direction;
    private Vector2 movePoint;

    public bool moveTrigger;

    public float speed;
    private float x, y;
	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Unit.Count; ++i)
        {
            if (Unit[i] == null)
            {
                Unit.RemoveAt(i);
            }
        }
        Move();
        if (Unit.Count <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    public void SetUnit()
    {
        unit_zako Unit_Zako;
        Unit = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selected_Unit"));
        speed = Unit[0].GetComponent<unit_zako>().Max_Speed;
        for (int i = 1; i < Unit.Count + 1; i++)//iはposの名前に利用するため1だけ増やしておく.
        {
            Unit_Zako = Unit[i - 1].GetComponent<unit_zako>();
            Unit[i - 1].transform.FindChild("selectCollider").GetComponent<unit_select>().IsSelected = false;
            Unit[i - 1].GetComponent<unit_zako>().ReTag();
            Unit_Zako.nowPhase = 0;
            Unit_Zako.Rush = transform.gameObject;
            Unit_Zako.RushPos = transform.FindChild("pos" + i).gameObject.transform.position;
            Unit_Zako.moveTrigger = true;
            Unit_Zako.set_speed(speed);
        }
    }

    public void Move()
    {
        unit_zako Unit_Zako;
        int ready=0;
        for (int i = 0; i < Unit.Count; i++)
        {
            if (Unit[i].GetComponent<unit_zako>().nowPhase == 1)
            {
                ready++;
            }
        }
        if (ready >= Unit.Count)
        {
            transform.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (Vector2.Distance(transform.position, movePoint) > speed/(speed*1.5))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        for (int i = 1; i < Unit.Count + 1; i++)
        {
            Unit_Zako = Unit[i - 1].GetComponent<unit_zako>();
            Unit_Zako.RushPos = transform.FindChild("pos" + i).gameObject.transform.position;
        }
        ready = 0;
    }

    public void SetPos(Vector2 newMovePoint)
    {
        movePoint = newMovePoint;
        x = movePoint.x;
        y = movePoint.y;
        direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;

    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("TP_Rush"))
        {
            MainCamera.GetComponent<Camera_Pinch>().SelectedObj = transform.gameObject;
            MainCamera.GetComponent<Camera_Pinch>().nowPhase = 1;
            GameObject.Find("RushSelectPlate").GetComponent<RushSelecter>().moveTrigger = true;
        }
    }

}
