using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;
using NCMB.Internal;

public class LogInManager : MonoBehaviour {

    public UserAuth userAuth;

    public string id;
    public string pw;
    public string mail;

	// Use this for initialization
	void Start (){
        userAuth = FindObjectOfType<UserAuth>();
        if (PlayerPrefs.GetString("id","null") == "null")
        {
            setUser();
            userAuth.signUp(id, pw);
        }
        else
        {
            userAuth.logIn(PlayerPrefs.GetString("id", "null"), PlayerPrefs.GetString("pw", "null"));
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setUser()
    {
        id = SystemInfo.deviceUniqueIdentifier;
        pw = "pw_" + 12345;
        PlayerPrefs.SetString("id", id);
        PlayerPrefs.SetString("pw", pw);
        PlayerPrefs.Save();
    }
}
