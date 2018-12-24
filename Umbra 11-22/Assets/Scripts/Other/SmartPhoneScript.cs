using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SmartPhoneScript : MonoBehaviour {

    // 메인 //
    public GameObject mainDisplay;
    public GameObject homeButton;
    bool mainActive = false;
   
    // 카메라 //
    public GameObject phoneCamera;
    public GameObject cameraDisplay;
    public GameObject cameraApp;
    public GameObject sccameraApp;
    bool cameraActive = false;

    // 플래시 // 
    public GameObject flashOnDisplay;
    public GameObject flashOffDisplay; 
    public GameObject flashApp;
    public GameObject scflashApp;
    public GameObject lightbutton;
    public GameObject phonelight;
    bool flashActive = false;

    // 갤러리 //
    public GameObject galleryDisplay;
    public GameObject galleryApp;
    public GameObject scgalleryApp;
    bool galleryActive = false;
    public GameObject[] savegallery = new GameObject[8];

    // 캡처 //
    public Camera cam;               // 저장할 카메라
    public GameObject saveLocation;  // 저장할 위치
    public GameObject ps;            // 이미지 맞추기 위한 변수
    int captureNum;                  // 몇개 저장했는지
    float delay = 2;                 // 캡쳐 딜레이

    // 배터리 //
    float btnlight = 3;
    float btncamera = 3;
    float normal= 1;
    public float bettery;

    // 스마트폰 //
    public Transform handLocation;
    public Transform controlLocation;
    bool phoneControl = false;

    // 기타 //
    public FirstPersonController Player;
    public Camera mainCamera;
    public GameObject Sample;

    void Start()
    {
        bettery = 10000;

        StartCoroutine(betteryCheck());
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {

        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }

        if(phoneControl)
        {
            MouseControl();         
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraDisplay.activeSelf)
            {
                StartCoroutine(capture());
            }
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            SetSmartPhone();
        }
    }

    void SetSmartPhone()
    {
        if (phoneControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Player.enabled = true;
            this.transform.position = handLocation.position;
            phoneControl = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Player.enabled = false;
            this.transform.position = controlLocation.position;
            phoneControl = true;
        }
    }

    void MouseControl()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            ///////////////////////////////////
            // 마우스 올렸을때 앱 커지는 효과//
            ///////////////////////////////////

            if (hit.collider.gameObject.name == cameraApp.name)
            {
                sccameraApp.SetActive(true);
                scflashApp.SetActive(false);
                scgalleryApp.SetActive(false);
            }

            if (hit.collider.gameObject.name == flashApp.name)
            {
                sccameraApp.SetActive(false);
                scflashApp.SetActive(true);
                scgalleryApp.SetActive(false);
            }

            if (hit.collider.gameObject.name == galleryApp.name)
            {
                sccameraApp.SetActive(false);
                scflashApp.SetActive(false);
                scgalleryApp.SetActive(true);
            }

            /////////////////////////////////////

            if (Input.GetMouseButtonDown(0))
            {
                // 메인화면서 카메라 화면으로 들어가기
                if (hit.collider.gameObject.name == sccameraApp.name)
                {
                    mainDisplay.SetActive(false);
                    cameraDisplay.SetActive(true);
                    phoneCamera.SetActive(true);
                    StartCoroutine(Effect(cameraDisplay));
                }

                // 메인화면에서 플래시화면들어가기
                 if (hit.collider.gameObject.name == scflashApp.name)
                {
                    mainDisplay.SetActive(false);
                    if (phonelight.activeSelf)
                    {
                        flashOnDisplay.SetActive(true);
                    }
                    else
                    {
                        flashOffDisplay.SetActive(true);
                    }
                    lightbutton.SetActive(true);
                    StartCoroutine(Effect(flashOffDisplay));
                }


                // 메인화면에서 갤러리들어가기
                if (hit.collider.gameObject.name == scgalleryApp.name)
                {
                    mainDisplay.SetActive(false);
                    galleryDisplay.SetActive(true);
                    StartCoroutine(Effect(galleryDisplay));                 
                }

                // 플래시 화면에서 전원켜기
                if (hit.collider.gameObject.name == lightbutton.name)
                {
                    if(flashOffDisplay.activeSelf)
                    {
                        phonelight.SetActive(true);
                        flashOnDisplay.SetActive(true);
                        flashOffDisplay.SetActive(false);
                    }
                    else
                    {
                        phonelight.SetActive(false);
                        flashOnDisplay.SetActive(false);
                        flashOffDisplay.SetActive(true);
                    }
                }

                // 모든 화면에서 홈메뉴로 돌아가기
                if(hit.collider.gameObject.name == homeButton.name)
                {
                    mainDisplay.SetActive(true);
                    cameraDisplay.SetActive(false);
                    phoneCamera.SetActive(false);
                    lightbutton.SetActive(false);
                    galleryDisplay.SetActive(false);
                    flashOffDisplay.SetActive(false);
                    flashOnDisplay.SetActive(false);
                    sccameraApp.SetActive(false);
                    scflashApp.SetActive(false);
                    scgalleryApp.SetActive(false);
                    StartCoroutine(Effect(mainDisplay)); 
                }
            }
        }
    }

    IEnumerator Effect(GameObject other)
    {
        Vector3 minScale = Sample.transform.localScale - new Vector3(0.5f, 0.5f, 0);
        Vector3 speed = new Vector3(0.05f, 0.05f, 0);

            other.transform.localScale = minScale;
            for (int i = 0; i < 10; i++)
            {
                other.transform.localScale += speed;
                yield return new WaitForSeconds(0.01f);
            }
    }

    IEnumerator capture()
    {
        RenderTexture rt = new RenderTexture(1024, 1024, 24);
        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;

        Texture2D sc = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
        sc.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
        sc.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;


        Sprite make = Sprite.Create(sc, new Rect(0, 0, sc.width, sc.height), new Vector2(0, 0));

        GameObject temp = new GameObject();

        temp.transform.SetParent(saveLocation.transform);
        temp.AddComponent<SpriteRenderer>();
        temp.GetComponent<SpriteRenderer>().sprite = make;
        temp.transform.position = ps.transform.position;
        temp.transform.localScale = ps.transform.localScale;
        temp.transform.localEulerAngles = new Vector3(0, 180, 0);
        temp.transform.name = "saveCapture" + captureNum;

        savegallery[captureNum].GetComponent<SpriteRenderer>().sprite = make;
        //savegallery[captureNum].transform.position = ps.transform.position;
        //savegallery[captureNum].transform.localScale = ps.transform.localScale;
        //savegallery[captureNum].transform.localEulerAngles = new Vector3(0, 180, 0);

        captureNum++;

        yield return new WaitForSeconds(1.5f);

        temp.SetActive(false);
    }

    public IEnumerator betteryCheck()
    {
        while (bettery > 0)
        {

            float add = btnlight + btncamera + normal;

            bettery -= add;

            yield return new WaitForSeconds(1);
        }
    }
}
