using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NCMB;
using KiiCorp.Cloud.Storage;
using MiniJSON;

public class Converter : MonoBehaviour {

    public bool IsConvert;
    public TextAsset CSVFile;
    public List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
    public List<IDictionary> WeaponData = new List<IDictionary>();
    private KiiObject WeaponObj = Kii.Bucket("WeaponData").NewKiiObject("WeaponData");
	// Use this for initialization
    void Start()
    {
        if (IsConvert)
        {
            Convert();
            Upload();
        }

	}

    private Dictionary<string,string> CreateDictionary(string[] tag,string[] itemValue){   //dictionary作成関数
        Dictionary<string,string> tmpDic = new Dictionary<string,string>();                     //tmpo

        for (int i=0; i < tag.Length; i++)     //タグごとに区切る
        {
            tmpDic.Add(tag[i], itemValue[i]);  //dictionaryに追加
        }
        return tmpDic;                              //返す
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Convert()
    {
        TextAsset csv = (Resources.Load(CSVFile.name, typeof(TextAsset)) as TextAsset);
        string text = csv.text;
        string[] wide = text.Split('\n');   //行ごとに改行で区切る
        string[] tag = wide[0].Split(',');      //最初の行からタグを取得する
        for (int i = 1; i < wide.Length; i++)   //2行目から
        {
            string[] tmpStr = wide[i].Split(',');       //数値ごとに,で区切る
            output.Add(CreateDictionary(tag, tmpStr));   //行のdictionaryを作る
        }
        WeaponObj["WeaponData"] = Json.Serialize(output);
    }

    public void Upload()
    {
        KiiObjectAcl acl = WeaponObj.Acl(ObjectAction.WRITE_EXISTING_OBJECT);
        KiiACLEntry<KiiObject, ObjectAction> aclEntry = acl.Subject(KiiUser.CurrentUser);
        aclEntry.Save(ACLOperation.REVOKE, (KiiACLEntry<KiiObject, ObjectAction> savedAclEntry, Exception e) =>
        {
            if (e != null)
            {
            }
        });

        WeaponObj.SaveAllFields(true, (KiiObject savedObj, Exception e) =>
{
    if (e != null)
    {
        Debug.LogWarning("WeaponData couldn't save");
        Debug.LogError(e);
    }
    else
    {
    }
});
    }
}
