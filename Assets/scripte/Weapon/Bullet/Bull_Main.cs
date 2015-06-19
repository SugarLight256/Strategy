using UnityEngine;
using System.Collections;

public class Bull_Main : MonoBehaviour
{
    private Vector2 direction;
    public Vector2 shotPos;

    public int atk;
    public int speed;

    public float range;
    // Use this for initialization
    void Start()
    {

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

    }

    public void SetSpeed(Vector2 targetPos)
    {
        float x, y;
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
