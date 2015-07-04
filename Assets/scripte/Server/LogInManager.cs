using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogInManager : MonoBehaviour {

    public UserAuth userAuth;
    public WeaponLoader weaponLoader;
    public UnitLoader unitLoader;

    public string id;
    public string pw;
    public string mail;

	// Use this for initialization
	void Start (){
        userAuth = FindObjectOfType<UserAuth>();
        weaponLoader = GameObject.Find("DataBase").GetComponent<WeaponLoader>();
        unitLoader = GameObject.Find("DataBase").GetComponent<UnitLoader>();
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
        if (userAuth.currentPlayer() != null && weaponLoader.WeaponData.Count > 0 && weaponLoader.WeaponBox.Count > 0 && unitLoader.UnitBox.Count > 0)
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
