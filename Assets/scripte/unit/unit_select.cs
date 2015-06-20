using UnityEngine;
using System.Collections;

public class unit_select : MonoBehaviour
{

    public bool IsSelected;
    public GameObject Rush_Manager;
    // Use this for initialization
    void Start()
    {
        IsSelected = false;
        Rush_Manager = GameObject.Find("RushSelecter").transform.FindChild("RushSelectPlate").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //IsSelected = true;
        transform.parent.GetComponent<unit_zako>().transform.tag = "Selected_Unit";
    }

}
