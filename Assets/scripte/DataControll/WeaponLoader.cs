using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using KiiCorp.Cloud.Storage;
using MiniJSON;

public class WeaponLoader : MonoBehaviour {

    public List<IDictionary> WeaponData = new List<IDictionary>();
	// Use this for initialization
    void Start()
    {


        WepDataLoad();
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void WepDataLoad()
    {
        Kii.Bucket("WeaponData").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
                Debug.Log(e);
            }
            else
            {
                for (int i = 0; i < ((IList)Json.Deserialize((string)result[0]["WeaponData"])).Count; i++)
                {
                    WeaponData.Add((IDictionary)((IList)Json.Deserialize((string)result[0]["WeaponData"]))[i]);
                }

                Debug.Log(WeaponData[1]["name"]);
            }
        });
    }
}
