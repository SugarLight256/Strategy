using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    private AsyncOperation tmpOperation = null;
    private bool changed;
    public bool allowChange;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (tmpOperation != null && !changed && tmpOperation.progress >= 0.9f && allowChange)
        {
            tmpOperation.allowSceneActivation = true;
            tmpOperation = null;
            changed = true;
            allowChange = false;
            print("a");
        }
	}

    public void sceneChange(string nextScene)
    {
        tmpOperation = Application.LoadLevelAsync(nextScene);
        tmpOperation.allowSceneActivation = false;
        changed = false;
    }

}
