using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if (userAuth.currentPlayer() != null && WeaponLoader.WeaponData.Count > 0 && WeaponLoader.WeaponBox.Count > 0 && UnitLoader.UnitBox.Count > 0)
        {
            Debug.Log("Load Scene : Menu");
            Application.LoadLevel("menu");
        }
	}

    public void setUser()
    {
        id = SystemInfo.deviceUniqueIdentifier;
        pw = SystemInfo.deviceName;
        PlayerPrefs.SetString("id", id);
        PlayerPrefs.SetString("pw", pw);
        PlayerPrefs.Save();
    }
}
