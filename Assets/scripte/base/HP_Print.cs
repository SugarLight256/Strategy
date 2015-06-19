using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HP_Print : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject baseColor;
    public GameObject Base;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = mainCamera.GetComponent<Camera>().WorldToScreenPoint(baseColor.transform.position);
        transform.localPosition = new Vector2(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2);
        transform.localScale = new Vector2(1000,1000) * (1/mainCamera.GetComponent<Camera>().orthographicSize);

        transform.GetComponent<Image>().fillAmount = Base.GetComponent<Base_Main>().HPper;
    }
}
