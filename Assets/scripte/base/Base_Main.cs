using UnityEngine;
using System.Collections;

public class Base_Main : MonoBehaviour {

    public GameObject blastShade;

    public int unitCount=0;
    public int maxHP;
    public int HP;
    public float HPper;
	// Use this for initialization
	void Start () {
        HP = maxHP;
        HPper = 100;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        --HP;
        HPper = (float)HP / maxHP;
        if (HP <= 0)
        {
            Instantiate(blastShade, transform.position, transform.rotation);
            Destroy(transform.gameObject);
        }
    }
}
