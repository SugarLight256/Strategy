using UnityEngine;
using System.Collections;

public class RushPlate : MonoBehaviour {

    public int knd;
    public GameObject Main_Camera;
    public GameObject RushManager;
    public GameObject[] Unit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (knd == 0)
        {

            Main_Camera.GetComponent<Camera_Pinch>().nowPhase = 0;
            transform.parent.GetComponent<RushSelecter>().closeTrigger = true;
            Unit = GameObject.FindGameObjectsWithTag("Selected_Unit");
            for (int i = 0; i < Unit.Length; i++)
            {
                Unit[i].GetComponent<unit_zako>().ReTag();
            }
        }
        else
        {
            Main_Camera.GetComponent<Camera_Pinch>().Rush_Maker = true;
            RushManager.GetComponent<RushManager>().NextKnd = knd;
        }
    }

}
