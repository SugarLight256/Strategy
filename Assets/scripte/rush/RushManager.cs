using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RushManager : MonoBehaviour {

    public int NextKnd;
	public GameObject[] Rusher;
    public List<GameObject> SelectedUnit;
	public GameObject RushBuff;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        SelectedUnit=new List<GameObject>(GameObject.FindGameObjectsWithTag("Selected_Unit"));
        if (SelectedUnit.Count >= 20)
        {
            for (int i = 20; i < SelectedUnit.Count; i++)
            {
                SelectedUnit[i].GetComponent<unit_zako>().ReTag();
                SelectedUnit.RemoveAt(i);
            }
        }
    }

    public void make_rush(Vector3 pos)
    {
        RushBuff = Instantiate(Rusher[NextKnd - 1], new Vector3(pos.x, pos.y, 0), Quaternion.identity) as GameObject;
        RushBuff.GetComponent<Rush>().IsPlayer = true;
        RushBuff.GetComponent<Rush>().SetUnit();
        RushBuff.transform.parent = transform;
	}
}
