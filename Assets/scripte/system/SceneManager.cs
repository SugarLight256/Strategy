using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void sceneChange(string nextScene)
    {
        Application.LoadLevel(nextScene);
    }

}
