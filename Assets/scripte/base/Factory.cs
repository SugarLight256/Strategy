using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public int coolMax;
    public bool IsPlayer;
	private int cool;
	public GameObject[] unit;
    public GameObject AI_Manager;
    private AI_Manager aiManager;

	// Use this for initialization
    void Start()
    {
        cool = 0;
        if (!IsPlayer)
        {
            aiManager = AI_Manager.GetComponent<AI_Manager>();
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (coolMax <= cool) {
			GameObject objTmp = GameObject.Instantiate (unit[0], transform.position, transform.rotation)as GameObject;
            GetComponent<Base_Main>().unitCount++;
            if (!IsPlayer)
            {
                aiManager.SetRushUnit(objTmp);
            }
            cool=0;
		}
		cool++;

	}
}
