using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using KiiCorp.Cloud.Storage;
using MiniJSON;

public class UnitLoader : MonoBehaviour {

    public static List<IDictionary> UnitData = new List<IDictionary>();
    public static List<IDictionary> UnitBox = new List<IDictionary>();
    public static IList FactoryUnit;
    private List<IDictionary> UnitBoxDef = new List<IDictionary>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UnitDataLoad()
    {
        Kii.Bucket("UnitData").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
                Debug.Log(e);
            }
            else
            {
                Debug.Log("UnitData Loading...");
                for (int i = 0; i < ((IList)Json.Deserialize((string)result[0]["UnitData"])).Count; i++)
                {
                    UnitData.Add((IDictionary)((IList)Json.Deserialize((string)result[0]["UnitData"]))[i]);
                }
                if (UnitData.Count > 0)
                    Debug.Log("Unit Data Load Succes");
            }
        });
    }

    public void UnitBoxLoad()
    {
        KiiUser.CurrentUser.Bucket("UnitBox").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
                Debug.Log(e);
            }
            else
            {
                if (result.Count > 0)
                {
                    Debug.Log("UnitBox Loading...");
                    for (int i = 0; i < ((IList)Json.Deserialize((string)result[0]["UnitBox"])).Count; i++)
                    {
                        UnitBox.Add((IDictionary)((IList)Json.Deserialize((string)result[0]["UnitBox"]))[i]);
                    }
                    if (UnitBox.Count > 0)
                    {
                        Debug.Log("UnitBox Load Succes");
                    }
                    else
                    {
                        Debug.LogWarning("UnitBox Load Failed");
                    }
                }
                else
                {
                    KiiBucket userBucket = KiiUser.CurrentUser.Bucket("UnitBox");
                    KiiObject boxObj = userBucket.NewKiiObject("UnitBox");
                    Kii.Bucket("UnitBoxDef").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result2, Exception e3) =>
                    {
                        Debug.Log("UnitBoxDef query result" + result2.Count);
                        if (e3 != null)
                        {
                            Debug.Log(e3);
                        }
                        else
                        {
                            for (int i = 0; i < ((IList)Json.Deserialize((string)result2[0]["UnitBoxDef"])).Count; i++)
                            {
                                UnitBoxDef.Add((IDictionary)((IList)Json.Deserialize((string)result2[0]["UnitBoxDef"]))[i]);
                            }
                            if (UnitBoxDef.Count > 0)
                            {
                                Debug.Log("UnitBox default data Load Succes");
                                boxObj["UnitBox"] = Json.Serialize(UnitBoxDef);
                                boxObj.SaveAllFields(true, (KiiObject savedObj, Exception e2) =>
                                {
                                    if (e != null)
                                    {
                                        Debug.LogWarning("UnitBox couldn't create");
                                        Debug.LogError(e2);
                                    }
                                    else
                                    {
                                        Debug.Log("UnitBox create Succes");
                                        UnitBoxLoad();
                                    }
                                });
                            }
                            else
                            {
                                Debug.LogWarning("UnitBox default data Load Failed");
                            }
                        }
                    });
                }
            }
        });
    }

    public void FactoryUnitLoad()
    {
        KiiUser.CurrentUser.Bucket("FactoryUnit").Query(new KiiQuery(), (KiiQueryResult<KiiObject> result, Exception e) =>
        {
            if (e != null)
            {
            }
            else
            {
                if (result.Count > 0)
                {
                    FactoryUnit = (IList)Json.Deserialize((string)result[0]["FactoryUnit"]);
                    Debug.Log("FactoryUnit Load Succes");
                }
                else
                {
                    KiiBucket userBucket = KiiUser.CurrentUser.Bucket("FactoryUnit");
                    KiiObject facObj = userBucket.NewKiiObject("FactoryUnit");
                    facObj["FactoryUnit"] = Json.Serialize( new List<int>{ 0 });
                    facObj.SaveAllFields(true, (KiiObject savedObj, Exception e2) =>
                    {
                        if (e != null)
                        {
                            Debug.LogWarning("FactoryUnit couldn't create");
                            Debug.LogError(e2);
                        }
                        else
                        {
                            Debug.Log("FactoryUnit create Succes");
                            FactoryUnitLoad();
                        }
                    });
                }
            }
        });


    }
}
