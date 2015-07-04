using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using KiiCorp.Cloud.Storage;
using MiniJSON;

public class WeaponLoader : MonoBehaviour {

    public static List<IDictionary> WeaponData = new List<IDictionary>();
    public static List<IDictionary> WeaponBox = new List<IDictionary>();
    private List<IDictionary> WeaponBoxDef = new List<IDictionary>();
	// Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this);
	    
	}
	
	// Update is called once per frame
	void Update () {
	


	}

    public void WepDataLoad()
    {
        Kii.Bucket("WeaponData").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
                Debug.Log(e);
            }
            else
            {
                Debug.Log("WeaponData Loading...");
                for (int i = 0; i < ((IList)Json.Deserialize((string)result[0]["WeaponData"])).Count; i++)
                {
                    WeaponData.Add((IDictionary)((IList)Json.Deserialize((string)result[0]["WeaponData"]))[i]);
                }
                if(WeaponData.Count > 0)
                Debug.Log("Weapon Data Load Succes");
            }
        });
    }

    public void WepBoxLoad()
    {
        KiiUser.CurrentUser.Bucket("WeaponBox").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
                Debug.Log(e);
            }
            else
            {
                if (result.Count > 0)
                {
                    Debug.Log("WeaponBox Loading...");
                    for (int i = 0; i < ((IList)Json.Deserialize((string)result[0]["WeaponBox"])).Count; i++)
                    {
                        WeaponBox.Add((IDictionary)((IList)Json.Deserialize((string)result[0]["WeaponBox"]))[i]);
                    }
                    if (WeaponBox.Count > 0)
                    {
                        Debug.Log("WeaponBox Load Succes");
                    }
                    else
                    {
                        Debug.LogWarning("WeaponBox Load Failed");
                    }
                }
                else
                {
                    KiiBucket userBucket = KiiUser.CurrentUser.Bucket("WeaponBox");
                    KiiObject boxObj = userBucket.NewKiiObject("WeaponBox");
                    Kii.Bucket("WeaponBoxDef").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result2, Exception e3) =>
                    {
                        Debug.Log("WeaponBoxDef query result" + result2.Count);
                        if (e3 != null)
                        {
                            Debug.Log(e3);
                        }
                        else
                        {
                            for (int i = 0; i < ((IList)Json.Deserialize((string)result2[0]["WeaponBoxDef"])).Count; i++)
                            {
                                WeaponBoxDef.Add((IDictionary)((IList)Json.Deserialize((string)result2[0]["WeaponBoxDef"]))[i]);
                            }
                            if (WeaponBoxDef.Count > 0)
                            {
                                Debug.Log("WeaponBox default data Load Succes");
                                boxObj["WeaponBox"] = Json.Serialize(WeaponBoxDef);
                                boxObj.SaveAllFields(true, (KiiObject savedObj, Exception e2) =>
                                {
                                    if (e != null)
                                    {
                                        Debug.LogWarning("WeaponBox couldn't create");
                                        Debug.LogError(e2);
                                    }
                                    else
                                    {
                                        Debug.Log("WeaponBox create Succes");
                                        WepBoxLoad();
                                    }
                                });
                            }
                            else
                            {
                                Debug.LogWarning("WeaponBox default data Load Failed");
                            }
                        }
                    });
                }
            }
        });
    }
}
