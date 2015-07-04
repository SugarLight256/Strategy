using UnityEngine;
using System.Collections;

public class Weapon_Main : MonoBehaviour
{
    public GameObject Bull;
    public GameObject target;
    private unit_zako unitZako;

    public int cool;
    public int maxCool;
    public int atk;

    public float range;

    public bool fire;
    // Use this for initialization
    void Start()
    {
        unitZako = transform.parent.GetComponent<unit_zako>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;

        if ((cool <= 0) && (fire == true) && (unitZako.bull > 0) && target != null)
        {
            cool = maxCool;
            Fire();
        }
        else if (fire == true && target != null && unitZako.bull > 0)
        {
            cool--;
        }
        else if (fire == false)
        {

        }
    }

    private void Fire()
    {
        (Instantiate(Bull, transform.position, transform.rotation) as GameObject).SendMessage("SetParent",this);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (fire == false || target==null)
        {
            fire = true;
            target = c.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        fire = false;
        target = null;
    }

}
