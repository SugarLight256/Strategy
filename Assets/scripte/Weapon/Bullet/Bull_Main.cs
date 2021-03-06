﻿using UnityEngine;
using System.Collections;

public class Bull_Main : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 shotPos;
    private GameObject target;
    private Weapon_Main parent;

    public int atk;
    public int speed;
    public int knd;

    public float range;
    // Use this for initialization
    void Start()
    {
        shotPos = transform.position;
        range = parent.range;
        target = parent.target;
        transform.tag = parent.tag;
        knd = parent.knd;
        GetComponent<SpriteRenderer>().color = parent.transform.parent.GetComponent<SpriteRenderer>().color;
        SetTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(shotPos, transform.position) > range+30)
        {
            Destroy(transform.gameObject);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        float x, y;
        Vector2 targetPos = target.transform.position;
        x = targetPos.x - transform.position.x;
        y = targetPos.y - transform.position.y;
        direction = new Vector2(x, y).normalized;
        transform.GetComponent<Rigidbody2D>().velocity = direction * speed;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    public void SetParent(Weapon_Main pr)
    {
        parent = pr;
    }
}
