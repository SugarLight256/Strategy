using UnityEngine;
using System.Collections;

public class RushManager : MonoBehaviour {

    public int NextKnd;
	public GameObject[] Rusher;
    public GameObject[] SelectedUnit;
	public GameObject RushBuff;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        SelectedUnit = GameObject.FindGameObjectsWithTag("Selected_Unit");
	}

    public void make_rush(Vector3 pos)
    {
        SelectedUnit = GameObject.FindGameObjectsWithTag("Selected_Unit");
        if (SelectedUnit.Length <= 20 && SelectedUnit.Length > 0)
        {
            RushBuff = Instantiate(Rusher[NextKnd - 1], new Vector3(pos.x, pos.y, 0), Quaternion.identity) as GameObject;
            RushBuff.GetComponent<Rush>().SetUnit();
            RushBuff.transform.parent = transform;
        }
	}
}
