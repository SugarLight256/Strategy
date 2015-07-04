using UnityEngine;
using System.Collections;

public class unit_zako : MonoBehaviour {

    public GameObject child;
    public GameObject Rush;
    public GameObject blastShard;
    public Vector2 RushPos;
    private Color defColor;

    public string def_tag;
    public string[] enemyBull;

    public int nowPhase=0;//0:単機行動 1:ラッシュ 2:追尾行動
    public int maxHP;
    public int HP;
    public int bull;
    public int maxBull;
    public int knd;

    public float speed;
    public float Max_Speed;

    public bool moveTrigger;
    // Use this for initialization
    void Start () {
        def_tag = transform.tag;
        HP = maxHP;
        bull = maxBull;
        defColor = GetComponent<SpriteRenderer>().color;
	}

    // Update is called once per frame
    void Update() {
        float x, y;
        SetColor();
        if (Rush != null)
        {
            Rush.GetComponent<Rush>().unitCount++;
            x = RushPos.x - transform.position.x;
            y = RushPos.y - transform.position.y;
        }
        else
        {
            x = 0;
            y = 0;

        }
        Vector2 direction = new Vector2(x, y).normalized;
        if (moveTrigger == true)
        {
            if (speed >= Max_Speed)
            {
                speed = Max_Speed;
            }
            transform.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
        if (Rush != null)
        {
            if (Vector2.Distance(transform.position, RushPos) < 0.04f*speed)
            {
                transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                transform.rotation = Rush.transform.rotation;
                transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
                nowPhase = 1;
                moveTrigger = false;
            }
            else
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
                moveTrigger = true;
            }
        }
	}

    public void set_speed(float s)
    {
        speed = s;
    }

    private void SetColor()
    {
        if (transform.tag == "Selected_Unit")
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = defColor;
        }
    }

    public void ReTag()
    {
        transform.tag = def_tag;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        for (int i = 0; i < enemyBull.Length; i++)
        {
            if (c.gameObject.transform.tag == enemyBull[i])
            {
                
                HP -= c.gameObject.GetComponent<Bull_Main>().atk;

                Destroy(c.gameObject);
            }
        }
        if (HP <= 0)
        {
            Instantiate(blastShard, transform.position, transform.rotation);
            Destroy(transform.gameObject);
        }
    }

}
