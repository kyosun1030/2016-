using UnityEngine;
using System.Collections;
using VRTK;

public class VRSmartPhoneScript : MonoBehaviour
{
    // 메인 //
    public GameObject mainDisplay;      // 화면
    public GameObject homeButton;       // 홈버튼
    //bool mainActive = false;

    // 카메라 //
    public GameObject phoneCamera;      // 폰에 나타낼 카메라
    public GameObject cameraDisplay;    // 폰 카메라 화면
    public GameObject cameraApp;        // 폰 카메라 앱
    public GameObject cameraOnApp;      // 폰 카메라 앱 배경
    //bool cameraActive = false;

    // 플래시 // 
    public GameObject flashApp;         // 플래시 앱
    public GameObject flashOnApp;       // 플래시 앱 배경
    public GameObject flashOnOff;       // 플래시가 켜져있는지 아닌지 보여주는 그림
    public GameObject phoneLight;       // 플래시         
    //bool flashActive = false;

    // 네비게이션 //
    public GameObject naviApp;          // 네비게이션 앱
    public GameObject naviOnApp;        // 네비게이션 앱 배경
    public GameObject naviDisplay;      // 네비에기션 디스플레이

    // 메시지 //
    public GameObject messageDisplay;   // 메시지 화면
    public GameObject messageApp;       // 메시지 앱 
    public GameObject messageOnApp;     // 메시지 
    public GameObject messageNoticeBar; // 메시지 알림 상단바
    public GameObject messageNotice;    // 메시지 알림

    // 전화 // 
    public GameObject phoneApp;         // 전화 앱
    public GameObject phoneOnApp;       // 전화 앱 배경

    // 배터리 //
    float btnlight = 3;                 // 플래시 켜면 더할 배터리 소모 수치
    float btncamera = 3;                // 카메라 켜면 더할 배터리 소모 수치
    float normal = 1;                   // 평소 배터리 소모 수치
    float add = 0;                      // 총 더할 값
    public float bettery;               // 남아있는 배터리
    public float betteryCount;          // 배터리를 숫자로 변환
    public GameObject betteryApp;

    // 기타 //
    //public GameObject Sample;                  
    public GameObject selectOn;                  // 패드로 무엇이 선택 되었는지 검은 배경

    public VRTK_ControllerEvents leftController; // 왼쪽 컨트롤러


    // up키 구현
    public bool tpPress = false;
    public bool first = false;

    
    // 터치패드 좌표 위치  X,Y -1~1로 구성되어있다. 
    // 중앙을 0,0 중심으로 왼쪽은 X = -1 , 오른쪽은 X = 1, 위쪽은 Y = 1, 아래쪽은 Y = -1
    public float tpX;
    public float tpY;

    void Start()
    {
        bettery = 10000;

        StartCoroutine(betteryCheck());

    }

    void Awake()
    {
        // 씬이 이동하더라도 오브젝트가 파괴되지말아라.
        DontDestroyOnLoad(transform.gameObject);
    }

    void FixedUpdate()
    {
        tpX = leftController.GetTouchpadAxis().x;               // 터치 패드 좌표 x
        tpY = leftController.GetTouchpadAxis().y;               // 터치 패드 좌표 y

        //print(leftController.GetTouchpadAxis());;


        // 터치패드를 누르고 있는 경우
        if (leftController.touchpadPressed)
        {
            first = true;
        }
        else if (first)
        {
            ActiveApp();
            first = false;
        }

        PhoneBGControl();

        // 스마트폰 컨트롤

    }

    void ActiveApp()
    {
        //////////////////////////////////
        // 터치를 눌렀을 때 실행 //
        //////////////////////////////////

        // 홈화면 일때만 실행 가능하게.
        if (mainDisplay.activeSelf)
        {
            // 패드 맨 위 (플래시)
            if (tpY > 0.7f && tpX > -0.2f && tpX < 0.2f)
            {
                if (!phoneLight.activeSelf)
                {
                    flashApp.SetActive(false);
                    flashOnOff.SetActive(true);
                    phoneLight.SetActive(true);
                }
                else
                {
                    flashApp.SetActive(true);
                    flashOnOff.SetActive(false);
                    phoneLight.SetActive(false);
                }
            }

            // 패드 왼쪽 위 (카메라)
            else if (tpX < 0 && tpY > 0 && tpY < 0.7f)
            {
                mainDisplay.SetActive(false);
                cameraDisplay.SetActive(true);
                messageDisplay.SetActive(false);
                naviDisplay.SetActive(false);
            }

            // 패드 오른쪽 위 (네비게이션)
            else if (tpX > 0 && tpY > 0 && tpY < 0.7f)
            {
                mainDisplay.SetActive(false);
                cameraDisplay.SetActive(false);
                messageDisplay.SetActive(false);
                naviDisplay.SetActive(true);
            }

            // 패드 왼쪽 밑 (전화)
            else if (tpX < 0 && tpY < 0)
            {
                //mainDisplay.SetActive(false);
                //cameraDisplay.SetActive(false);
                //messageDisplay.SetActive(false);
                //naviDisplay.SetActive(false);
            }

            // 패드 오른쪽 밑 (메시지)
            else if (tpX > 0 && tpY < 0)
            {
                mainDisplay.SetActive(false);
                cameraDisplay.SetActive(false);
                naviDisplay.SetActive(false);
                messageDisplay.SetActive(true);

                if (messageNoticeBar.activeSelf)
                {
                    messageNoticeBar.SetActive(false);
                }

                if (messageNotice.activeSelf)
                {
                    messageNotice.SetActive(false);
                }
            }
        }
    }

    void PhoneBGControl()
    {
        // 터치가 되고 있는 경우.
            // 패드 맨 위 (플래시)
            if (tpY > 0.6f && tpX > -0.4f && tpX < 0.4f)
            {
                flashOnApp.SetActive(true);
                cameraOnApp.SetActive(false);
                naviOnApp.SetActive(false);
                phoneOnApp.SetActive(false);
                messageOnApp.SetActive(false);
            }

            // 패드 왼쪽 위 (카메라)
            else if (tpX < 0 && tpY > 0 && tpY < 0.6f)
            {
                flashOnApp.SetActive(false);
                cameraOnApp.SetActive(true);
                naviOnApp.SetActive(false);
                phoneOnApp.SetActive(false);
                messageOnApp.SetActive(false);
            }

            // 패드 오른쪽 위 (네비게이션)
            else if (tpX > 0 && tpY > 0 && tpY < 0.6f)
            {
                flashOnApp.SetActive(false);
                cameraOnApp.SetActive(false);
                naviOnApp.SetActive(true);
                phoneOnApp.SetActive(false);
                messageOnApp.SetActive(false);
            }

            // 패드 왼쪽 밑 (전화)
            else if (tpX < 0 && tpY < -0.2f)
            {
                flashOnApp.SetActive(false);
                cameraOnApp.SetActive(false);
                naviOnApp.SetActive(false);
                phoneOnApp.SetActive(true);
                messageOnApp.SetActive(false);
            }

            // 패드 오른쪽 밑 (메시지)
            else if (tpX > 0 && tpY < -0.2f)
            {
                flashOnApp.SetActive(false);
                cameraOnApp.SetActive(false);
                naviOnApp.SetActive(false);
                phoneOnApp.SetActive(false);
                messageOnApp.SetActive(true);
            }

            //print(leftController.touchpadTouched);
            //print(leftController.touchpadPressed);

        // 모든 화면에서 홈메뉴로 돌아가기
        if (leftController.triggerClicked)
        {
            mainDisplay.SetActive(true);
            cameraDisplay.SetActive(false);
            messageDisplay.SetActive(false);
            naviDisplay.SetActive(false);
            //StartCoroutine(Effect(mainDisplay));
        }
    }

    public IEnumerator betteryCheck()
    {
        while (bettery > 0)
        {
            add = 0;
            add = btnlight + btncamera + normal;

            bettery -= add;

            betteryCount = bettery * 0.0001f;

            betteryApp.transform.localScale = new Vector3(1f,betteryCount,1f);
            betteryApp.transform.localPosition = new Vector3(0, (1 - betteryCount) * -1 / 2, 0);

            yield return new WaitForSeconds(1);
        }
    }
}

//// 무엇이선택되었는지 앱이 표시되게.
//void SelectOnBG(GameObject obj)
//{
//    // 배경이 나타낼 x,y좌표와 현재 배경은 z을 고정
//    selectOn.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,selectOn.transform.position.z);
//    // 배경이 활성화되어있지 않을경우 활성화시킨다.
//    if (!selectOn.activeSelf)
//    {
//        selectOn.SetActive(true);
//    }
//}

/////////////////////////////////
// 마우스 올렸을때 앱 커지는 효과//
/////////////////////////////////

//IEnumerator Effect(GameObject other)
//{
//    Vector3 minScale = Sample.transform.localScale - new Vector3(0.5f, 0.5f, 0);
//    Vector3 speed = new Vector3(0.05f, 0.05f, 0);

//    yield return new WaitForSeconds(0.01f);

//    //other.transform.localScale = minScale;
//    //for (int i = 0; i < 10; i++)
//    //{
//    //    other.transform.localScale += speed;
//    //    yield return new WaitForSeconds(0.01f);
//    //}
//}

//IEnumerator capture()
//{
//    RenderTexture rt = new RenderTexture(1024, 1024, 24);
//    cam.targetTexture = rt;
//    cam.Render();

//    RenderTexture.active = rt;

//    Texture2D sc = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
//    sc.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
//    sc.Apply();

//    cam.targetTexture = null;
//    RenderTexture.active = null;


//    Sprite make = Sprite.Create(sc, new Rect(0, 0, sc.width, sc.height), new Vector2(0, 0));

//    GameObject temp = new GameObject();

//    temp.transform.SetParent(saveLocation.transform);
//    temp.AddComponent<SpriteRenderer>();
//    temp.GetComponent<SpriteRenderer>().sprite = make;
//    temp.transform.position = ps.transform.position;
//    temp.transform.localScale = ps.transform.localScale;
//    temp.transform.localEulerAngles = new Vector3(0, 180, 0);
//    temp.transform.name = "saveCapture" + captureNum;

//    savegallery[captureNum].GetComponent<SpriteRenderer>().sprite = make;

//    captureNum++;

//    yield return new WaitForSeconds(1.5f);

//    temp.SetActive(false);
//}

