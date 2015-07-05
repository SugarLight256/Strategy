using UnityEngine;
using System.Collections;

public class Base_Main : MonoBehaviour {

    public GameObject blastShade;
    public GameObject sceneManager;

    public int unitCount=0;
    public int maxHP;
    public int HP;
    public float HPper;
    public int maxBull;
    public int bull;
    public float bullPer;

    private float destroyCount;
	// Use this for initialization
	void Start () {
        HP = maxHP;
        HPper = 100;
        bull = maxBull;
        bullPer = 100;
        sceneManager = GameObject.Find("SceneManager");
	}
	
	// Update is called once per frame
	void Update () {
        if (HP<=0)
        {
            if (destroyCount % 50 == 0)
            {
                float x, y;
                x = transform.position.x - 50;
                y = transform.position.y - 50;
                Vector2 instPos = new Vector2(x + 100 * Random.value, y + 100 * Random.value);
                Instantiate(blastShade, instPos, transform.rotation);
                if (destroyCount/100 >= 7)
                {
                    Destroy(transform.gameObject);
                    if (transform.gameObject.layer == LayerMask.NameToLayer("GREEN_Base"))
                    {
                        sceneManager.GetComponent<SceneManager>().sceneChange("menu");
                    }
                    print("Destroy");
                }
            }
            destroyCount += 1;
        }
	}

    public void ReBullPer()
    {
        bullPer = (float)bull / maxBull;
    }

    public void HPCalc(int atk)
    {
        HP -= atk;
        HPper = (float)HP / maxHP;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
    }
}
