using UnityEngine;

public class Camera_Pinch : MonoBehaviour {
	
	public  Camera m_Camera;

	public  GameObject selectBox;
    public  GameObject RushSelecter;
    public  GameObject Rush_Manager;
    public  GameObject SelectedObj;
    public  GameObject TP_UI;
    public  GameObject TP_Normal;
	private GameObject SelectBox;

	private bool IsPinched  = false;
	private bool IsSelect   = false;
	private bool IsTap      = false;
    public  bool Rush_Maker = false;

    public int nowPhase=0;//今の状態0:特になし 1:セレクトパネル表示中 2:Rush移動座標指定 3:詳細表示中.

	public  float selectTime  = 0.1f;
	private float timer       = 0.0f;
	private float pinchLength = 0.0f;

	private Vector3 touchPos;
	private Vector3 sb_position;
	private	Vector3 screenPos      = new Vector3(0,0,  0);
	private Vector3 deltaScreenPos = new Vector3(0,0,-10);

	void Start(){
	}

    void Update()
    {
        /*if (SelectedObj != null && SelectedObj.tag == "Rush")
        {
            transform.position = new Vector3(SelectedObj.transform.position.x, SelectedObj.transform.position.y, -10);
        }*/
        Vector3 position = transform.position;
        if (Input.touchCount > 0)
        {
            if (IsSelect == true && Input.touches[0].phase == TouchPhase.Ended)
            {
                RushSelecter.GetComponent<RushSelecter>().moveTrigger = true;
                nowPhase = 1;
                IsSelect = false;
            }
            if (nowPhase == 2 || nowPhase == 0)
            {
                RushSelecter.GetComponent<RushSelecter>().closeTrigger = true;

            }
            TP_Normal.layer = LayerMask.NameToLayer("TP_Rush");
        }
        if (Input.touchCount >= 1)
        {
            touchPos = m_Camera.ScreenToWorldPoint(Input.touches[0].position);
        }
        if (Input.touchCount <= 0)
        {
            IsSelect = false;
            IsPinched = false;
            TP_UI.layer = LayerMask.NameToLayer("NoCollider");
            TP_Normal.layer = LayerMask.NameToLayer("NoCollider");
            Destroy(SelectBox);
        }
        else if (Input.touchCount == 2)
        {//二本以上で拡大縮小処理.

            Vector2 touchMidPos = new Vector2(0, 0);

            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
            {
                pinchLength = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            }
            else if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
            {
                float length = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                float scale = length / pinchLength;

                m_Camera.orthographicSize += ((pinchLength - length) / 2);

                touchMidPos = (Input.touches[0].position + Input.touches[1].position) * 0.5f;
                touchMidPos = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchMidPos.x, touchMidPos.y, 0));

                Vector2 diff = touchMidPos - new Vector2(transform.position.x, transform.position.y);
                scale = 1.0f - scale;
                diff = diff * scale;

                transform.position -= new Vector3(diff.x, diff.y, 0);

                float fMinSize = 50.0f;
                float fMaxSize = 1000.0f;

                m_Camera.orthographicSize = Mathf.Clamp(m_Camera.orthographicSize, fMinSize, fMaxSize);

                pinchLength = length;
                IsPinched = true;

            }
        }
        else if (Input.touchCount == 1 && IsPinched != true)
        {//拡大中ではなく一本タッチでスライドまたはダブルタッチでセレクト.
            if (Input.touches[0].phase == TouchPhase.Began)
            {

                if (IsTap == true && timer <= selectTime)
                {//セレクトBox表示.

                    IsSelect = true;
                    IsTap = false;
                    SelectBox = Instantiate(selectBox, new Vector3(touchPos.x, touchPos.y, 0), transform.rotation) as GameObject;
                    sb_position = new Vector3(touchPos.x, touchPos.y, 0);

                }
                else
                {
                    IsTap = false;
                }

                switch (nowPhase)
                {
                    case 0://特になし.
                        break;
                    case 1://セレクトパネル表示中.
                        if (Rush_Maker == true)//もしラッシュ作成Tureなら作成.
                        {
                            Rush_Manager.GetComponent<RushManager>().make_rush(touchPos);
                            Rush_Maker = false;
                            nowPhase = 0;
                        }
                        else if (Rush_Manager.GetComponent<RushManager>().SelectedUnit.Count == 0 && GameObject.FindGameObjectsWithTag("Rush").Length > 0)
                        {
                            TP_Normal.layer = LayerMask.NameToLayer("TP_Rush");
                            TP_UI.layer = LayerMask.NameToLayer("NoCollider");
                            if (SelectedObj != null)
                            {
                                if (SelectedObj.transform.tag == "Rush" && Input.touches[0].position.y > m_Camera.WorldToScreenPoint(m_Camera.ScreenToWorldPoint(new Vector3(0,0,0))+new Vector3(0,75,0)).y)
                                {
                                    SelectedObj.GetComponent<Rush>().SetPos(touchPos);
                                    SelectedObj = null;
                                }
                            }
                            nowPhase = 0;
                        }
                        else if (Rush_Manager.GetComponent<RushManager>().SelectedUnit.Count > 0)
                        {
                            TP_UI.layer = LayerMask.NameToLayer("TP_UI");
                        }
                        else
                        {
                            nowPhase = 0;
                        }
                        break;

                    case 2://Rush移動地点指定中.
                        if (SelectedObj != null)
                        {
                            if (SelectedObj.transform.tag == "Rush")
                            {
                                SelectedObj.GetComponent<Rush>().SetPos(touchPos);
                            }
                        }
                        nowPhase = 0;
                        break;
                    case 3://詳細表示中.
                        break;
                }

                timer = 0.0f;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (timer <= selectTime && IsSelect == false)
                {
                    IsTap = true;
                }
                timer = 0.0f;

            }
            if (IsSelect)
            {//セレクト中なら.
                SelectBox.transform.localScale = sb_position - new Vector3(touchPos.x, touchPos.y, 0);
            }
            else if (IsSelect != true)
            {//セレクト中でないなら.

                if (Input.touches[0].phase == TouchPhase.Began)
                {//タッチ開始時の座標を保存.
                    screenPos = GetComponent<Camera>().ScreenToWorldPoint(Input.touches[0].position);
                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {//指を動かしたなら.
                    deltaScreenPos = screenPos - GetComponent<Camera>().ScreenToWorldPoint(Input.touches[0].position);//前回のタッチ座標との差分を求める.
                    position.x += deltaScreenPos.x;
                    position.y += deltaScreenPos.y;
                    transform.position = position;//移動.
                    screenPos = GetComponent<Camera>().ScreenToWorldPoint(Input.touches[0].position);//タッチ座標の保存.
                }
            }
        }

        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            timer = 10.0f;
        }
    }
}