using UnityEngine;
using System.Collections;

public class Weapon_Main : MonoBehaviour
{
    public GameObject Bull;
    private GameObject target;

    public int bull;
    public int maxBull;
    public int cool;
    public int maxCool;

    public float range;

    public bool fire;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;

        if ((cool >= maxCool) && (fire == true) && (bull > 0) && target != null)
        {
            cool = 0;
            Fire();
        }
        else if (fire == true && target != null && bull > 0)
        {
            ++cool;
        }
        else if (fire == false)
        {

        }
    }

    private void Fire()
    {
        Bull_Main tmpCmp;
        tmpCmp =(Instantiate(Bull, transform.position, transform.rotation) as GameObject).GetComponent<Bull_Main>();
        bull--;
        tmpCmp.SetSpeed(target.transform.position);
        tmpCmp.range = range;
        tmpCmp.shotPos = transform.position;
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
