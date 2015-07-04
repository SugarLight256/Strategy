using UnityEngine;
using System.Collections;
using System;
using KiiCorp.Cloud;
using KiiCorp.Cloud.Storage;

public class UserAuth : MonoBehaviour {

    private string currentPlayerName = null;
    private UserAuth instance = null;
    private bool isLoggingin = false;
    private GameObject DataBase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            string name = gameObject.name;
            gameObject.name = name + "(Singleton)";

            GameObject duplicater = GameObject.Find(name);
            if (duplicater != null)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.name = name;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        DataBase = GameObject.Find("DataBase");
    }
    
    public void logIn(string id, string pw)
    {
        KiiUser.LogIn(id, pw, (KiiUser user, Exception e) =>
        {
            if (e == null)
            {
                Debug.Log("Log In With:" + id);
                currentPlayerName = id;
                DataBase.GetComponent<WeaponLoader>().WepDataLoad();
                DataBase.GetComponent<WeaponLoader>().WepBoxLoad();
                DataBase.GetComponent<UnitLoader>().UnitDataLoad();
                DataBase.GetComponent<UnitLoader>().UnitBoxLoad();
                DataBase.GetComponent<UnitLoader>().FactoryUnitLoad();
            }
            else
            {
                Debug.LogWarning("Log In Failed");
                logIn(id, pw);
            }
        });
    }
    public void signUp(string id, string pw)
    {
        if (!isLoggingin)
        {
            KiiUser.Builder builder;
            builder = KiiUser.BuilderWithName(id);
            KiiUser user = builder.Build();
            user.Register(pw,(KiiUser registeredUser,Exception e)=>
            {
                if (e == null)
                {
                    Debug.Log("Sign In With:" + id);
                    currentPlayerName = id;
                    isLoggingin = false;
                    DataBase.GetComponent<WeaponLoader>().WepDataLoad();
                    DataBase.GetComponent<WeaponLoader>().WepBoxLoad();
                    DataBase.GetComponent<UnitLoader>().UnitDataLoad();
                    DataBase.GetComponent<UnitLoader>().UnitBoxLoad();
                    DataBase.GetComponent<UnitLoader>().FactoryUnitLoad();
                }
                else
                {
                    Debug.Log(e);
                    isLoggingin = false;
                }
            });
            isLoggingin = true;
        }
    }

    // 現在のプレイヤー名を返す --------------------
    public string currentPlayer()
    {
        return currentPlayerName;
    }
}
