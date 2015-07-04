using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public int coolMax;
    public bool IsPlayer;
	private int cool;
	private GameObject[] unit = new GameObject[5];
    public GameObject[] pref = new GameObject[5];
    public GameObject AI_Manager;
    public int Max_Unit;
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
        if (IsPlayer)
        {
            CreateMother();
        }
        else
        {
            unit = pref;
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (coolMax <= cool && baseMain.bull > 0 && baseMain.unitCount<=100 ) {
			GameObject objTmp = GameObject.Instantiate (unit[0], transform.position, transform.rotation)as GameObject;
            baseMain.unitCount++;
            objTmp.SetActive(true);
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

    private void CreateMother()
    {
        for (int i = 0; i < unit.Length; i++)
        {
            unit[i] = Instantiate(pref[i], transform.position, transform.rotation) as GameObject;
            unit[i].SetActive(false);
            unit_zako unitMain = unit[i].GetComponent<unit_zako>();
            if (int.Parse(UnitLoader.FactoryUnit[i].ToString()) != -1)
            {
                int facUnit = int.Parse(UnitLoader.FactoryUnit[i].ToString());
                int knd = int.Parse((string)(UnitLoader.UnitBox[facUnit]["Knd"]))-1;

                unitMain.maxBull = int.Parse((string)(UnitLoader.UnitBox[facUnit]["Max_Bull"]));
                unitMain.maxBull += int.Parse((string)(UnitLoader.UnitData[knd]["Max_Bull"]));

                unitMain.Max_Speed = int.Parse((string)(UnitLoader.UnitBox[facUnit]["Speed"]));
                unitMain.Max_Speed += int.Parse((string)(UnitLoader.UnitData[knd]["speed"]));

                unitMain.maxHP = int.Parse((string)(UnitLoader.UnitBox[facUnit]["HP"]));
                unitMain.maxHP += int.Parse((string)(UnitLoader.UnitData[knd]["HP"]));

                unitMain.knd = int.Parse((string)(UnitLoader.UnitBox[facUnit]["Knd"]));

                unitMain.name = (string)(UnitLoader.UnitData[knd]["name"]);
            }
            else
            {

            }

        }
    }
}
