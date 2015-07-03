using UnityEngine;
using System.Collections;
using System;
using KiiCorp.Cloud;
using KiiCorp.Cloud.Storage;

public class UserAuth : MonoBehaviour {

    private string currentPlayerName;
    private UserAuth instance = null;
    private bool isLoggingin = false;

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
    }
    
    public void logIn(string id, string pw)
    {
        KiiUser.LogIn(id, pw, (KiiUser user, Exception e) =>
        {
            if (e == null)
            {
                Debug.Log("Log In With:" + id);
                currentPlayerName = id;
                Application.LoadLevel("menu");
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
            Debug.Log("asd");
            user.Register(pw,(KiiUser registeredUser,Exception e)=>
            {
                if (e == null)
                {
                    Debug.Log("Sign In With:" + id);
                    currentPlayerName = id;
                    Application.LoadLevel("menu");
                    isLoggingin = false;
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
