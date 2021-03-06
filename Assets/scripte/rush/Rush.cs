﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rush : MonoBehaviour {

    public List<GameObject> Unit;
    public List<unit_zako> zako;

    public Camera_Pinch MainCamera;
    public GameObject target;
    public GameObject aiManager;

    private Vector2 direction;
    private Vector2 movePoint;

    public bool moveTrigger;
    public bool IsPlayer;

    public int unitCount;

    public float speed;
    private float x, y;
	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera_Pinch>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < zako.Count; i++)
        {
            if (Unit[i] == null || zako[i] == null)
            {
                Unit.RemoveAt(i);
                zako.RemoveAt(i);
            }
            else if (zako[i].Rush != transform.gameObject)
            {
                Unit.RemoveAt(i);
                zako.RemoveAt(i);
            }

        }
        if (unitCount <= 0)
        {
            Destroy(transform.gameObject);
        }
        if (aiManager != null && target == null)
        {
            SetPos(aiManager.GetComponent<AI_Manager>().SetRushGo());
        }
        Move();
        unitCount = 0;
    }

    public void SetUnit(List<GameObject> unitTmp)
    {
        unit_zako Unit_Zako;
        Unit =new List<GameObject>(unitTmp);
        if (Unit[0] != null)
        {
            speed = Unit[0].GetComponent<unit_zako>().Max_Speed;
            for (int i = 1; i < Unit.Count +1; i++)//iは3行下のposの名前に利用するため1だけ増やしておく.
            {
                if (Unit[i - 1] != null)
                {
                    zako.Add(Unit[i - 1].GetComponent<unit_zako>());
                    Unit_Zako = Unit[i - 1].GetComponent<unit_zako>();
                    Unit_Zako.RushPos = transform.FindChild("pos" + i).gameObject.transform.position;
                    Unit_Zako.nowPhase = 0;
                    Unit_Zako.Rush = transform.gameObject;
                    Unit_Zako.moveTrigger = true;
                    Unit_Zako.set_speed(speed);
                    if (Unit[i - 1].transform.tag == "GREEN_Unit")
                    {
                        Unit[i - 1].transform.FindChild("selectCollider").GetComponent<unit_select>().IsSelected = false;
                    }
                    Unit[i - 1].GetComponent<unit_zako>().ReTag();
                }
            }
        }
    }

    public void Move()
    {
        int ready=0;
        for (int i = 0; i < zako.Count; i++)
        {
            if (Unit[i] != null && zako[i].nowPhase == 1)
            {
                ready++;
            }
        }
        if (target != null)
        {
            movePoint = target.transform.position;
            x = movePoint.x;
            y = movePoint.y;
            direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
        }
        if (ready >= Unit.Count && Vector2.Distance(transform.position, movePoint) > 0.1*speed)
        {
            transform.GetComponent<Rigidbody2D>().velocity = direction * speed;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        for (int i = 1; i < zako.Count + 1; i++)
        {
            if (Unit[i-1] != null)
            {
                zako[i-1].RushPos = transform.FindChild("pos" + i).gameObject.transform.position;
            }
        }
        ready = 0;
    }

    public void SetPos(GameObject nextTarget)
    {
        target = nextTarget;
        movePoint = target.transform.position;
        x = movePoint.x;
        y = movePoint.y;
        direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        if (!IsPlayer)
        {
            transform.tag = "Enemy_Rush";
        }
    }

    public void SetPos(Vector2 nextTarget)
    {
        movePoint = nextTarget;
        x = movePoint.x;
        y = movePoint.y;
        direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        if (!IsPlayer)
        {
            transform.tag = "Enemy_Rush";
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (IsPlayer && c.gameObject.layer == LayerMask.NameToLayer("TP_Rush"))
        {
            MainCamera.SelectedObj = transform.gameObject;
            MainCamera.nowPhase = 1;
            MainCamera.TP_UI.layer = LayerMask.NameToLayer("NoCollider");
            GameObject.Find("RushSelectPlate").GetComponent<RushSelecter>().moveTrigger = true;
        }
        else if(!IsPlayer && c.gameObject.layer == LayerMask.NameToLayer("TP_Rush"))
        {
            MainCamera.SelectedObj = transform.gameObject;
            MainCamera.nowPhase = 1;
        }
    }

}
