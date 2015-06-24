using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public int coolMax;
    public bool IsPlayer;
	private int cool;
	public GameObject[] unit;
    public GameObject AI_Manager;
    private Base_Main baseMain;
    private AI_Manager aiManager;
    private unit_zako unitZako;

	// Use this for initialization
    void Start()
    {
        cool = 0;
        if (!IsPlayer)
        {
            aiManager = AI_Manager.GetComponent<AI_Manager>();
        }
        baseMain = GetComponent<Base_Main>();

    }
	
	// Update is called once per frame
	void Update () {

		if (coolMax <= cool && baseMain.bull > 0 && baseMain.unitCount<=100) {
			GameObject objTmp = GameObject.Instantiate (unit[0], transform.position, transform.rotation)as GameObject;
            baseMain.unitCount++;
            unitZako = objTmp.GetComponent<unit_zako>();
            if (baseMain.bull > unitZako.maxBull)
            {
                baseMain.bull -= unitZako.maxBull;
            }
            else
            {
                unitZako.maxBull = baseMain.bull;
                baseMain.bull = 0;
            }
            baseMain.ReBullPer();
            if (!IsPlayer)
            {
                aiManager.SetRushUnit(objTmp);
            }
            cool=0;
		}
		cool++;

	}
}
