﻿using UnityEngine;
using System.Collections;

public class Bull_Main : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 shotPos;
    private GameObject target;

    public int atk;
    public int speed;
    public int knd;

    public float range;
    // Use this for initialization
    void Start()
    {
        shotPos = transform.position;
        range = int.Parse((string)WeaponLoader.WeaponData[0]["range"]);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Vector2.Distance(shotPos, transform.position) > range+30)
        {
            Destroy(transform.gameObject);
        }
    }

    private void Move()
    {
        switch (knd)
        {
            case 1:
                break;
            default:
                break;
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

    void OnTriggerEnter2D(Collider2D c)
    {
        Destroy(transform.gameObject);
    }

}
