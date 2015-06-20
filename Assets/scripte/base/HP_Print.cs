using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HP_Print : MonoBehaviour {

    public GameObject Base;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Base != null)
        {
            transform.GetComponent<Image>().fillAmount = Base.GetComponent<Base_Main>().HPper;
        }
        else
        {
            transform.GetComponent<Image>().fillAmount = 0;
        }
    }
}
