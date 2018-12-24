using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRTK;
public class OpEventScript : MonoBehaviour {

    //
    /* 이 스크립트 상에서 모든 이벤트를 제어 할 것 */
    //
    public BGMPlayerScript bgmScript;

    public VRTK_TouchpadWalking touchWalkScript;

    public GameObject player;

    // 시간 이벤트를 위해 게임이 시작한 후 시간을 잰다.
    public float gameTime;

    // 일시정지 구상 중
    public bool isPause = false;

    //
    /* 씬처음 */
    //
    public SceneNameScript sceneScript;
    public SceneNameScript sceneScript2;
    public GameObject phone;

    //
    /* 튜토리얼*/
    // 
    public bool Event1 = false;       // 일단 논리값 하나 생성

    public GameObject messageApp;
    public GameObject naviApp;
    public GameObject cameraApp;

    public GameObject tutoObjective;
    public GameObject gamestart;
    public GameObject[] tips;

    public bool messageAppOn = false;
    public bool naviAppOn = false;
    public bool cameraAppOn = false;

    public GameObject messageAppActive;
    public GameObject naviAppActive;
    public GameObject cameraAppActive;

    //
    /* 이벤트 1 */
    //

    public GameObject wallWord;
    public GameObject event1Navi;


    //
    /* 이벤트 2 */
    //
    public bool event2Start;
    public GameObject Event2;

    public GameObject manequin1;
    public GameObject manequin2;
    public GameObject manequin3;

    public GameObject dirLight;
    public GameObject event2Navi;
    private bool redLightRe = true;

    // 블랙 스크린
    public GameObject black;

    public SceneManagerScript sceneloadScript;

    // 원하는 이벤트 만큼 추가 시킬것 

    void Start()
    {
        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<BGMPlayerScript>();
        sceneScript = GameObject.Find("SceneName").GetComponent<SceneNameScript>();
        sceneScript2 = GameObject.Find("SceneName2").GetComponent<SceneNameScript>();
        gameTime = 0;

        // 홀
        //async = Application.LoadLevelAsync(2);
        //async.allowSceneActivation = false;
        //bgmScript.StartCoroutine(bgmScript.Player(0, false));

        touchWalkScript.maxWalkSpeed = 0;

        StartCoroutine(First());
        //StartCoroutine(Event2On());
    }

    void FixedUpdate()
    {
        if (wallWord.GetComponent<CameraInScript>().Complete && !event2Start)
        {
            bgmScript.PlayEffect(5, false);
            event1Navi.SetActive(false);
            Event2.SetActive(true);
            event2Navi.SetActive(true);
            event2Start = true;
            //wallWord.GetComponent<CameraInScript>().Complete = false;
        }
    }

    IEnumerator First()
    {
        yield return new WaitForSeconds(1);

        sceneScript.TextCopyStart("제 0 장", 3);

        yield return new WaitForSeconds(2);

        sceneScript2.TextCopyStart("초대", 1);

        yield return new WaitForSeconds(3);

        StartCoroutine(FirstFade(black));

       //yield return new WaitForSeconds(1);

        phone.SetActive(true);

        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        if (messageApp.activeSelf && !messageAppOn)
        {
            messageAppOn = true;
            messageAppActive.GetComponent<TextMesh>().text = "메시지 1/1";
            bgmScript.PlayEffect(4, false);
        }
        else if (naviApp.activeSelf && !naviAppOn)
        {
            naviAppOn = true;
            naviAppActive.GetComponent<TextMesh>().text = "네비게이션 1/1";
            bgmScript.PlayEffect(4, false);
        }
        else if (cameraApp.activeSelf && !cameraAppOn)
        {
            cameraAppOn = true;
            cameraAppActive.GetComponent<TextMesh>().text = "카메라 1/1";
            bgmScript.PlayEffect(4, false);
        }

        yield return null;

        if (messageAppOn && naviAppOn && cameraAppOn)
        {
            //tutoObjective.SetActive(false);
            for (int i = 0; i < tips.Length - 1; i++)
            {
                tips[i].SetActive(false);
            }

            yield return new WaitForSeconds(1);

            gamestart.SetActive(true);

            yield return new WaitForSeconds(4);

            gamestart.SetActive(false);
        }
        else
        {
            StartCoroutine(Tutorial());
        }
    }

    public void Event2Start()
    {
        StartCoroutine(Event2Move());
    }

    IEnumerator Event2Move()
    {
        touchWalkScript.maxWalkSpeed = 0;

        StartCoroutine(FadeInOut(black, 2));

        yield return new WaitForSeconds(1);

        player.transform.position = event2Navi.transform.position;

        yield return new WaitForSeconds(2);

        StartCoroutine(Event2On());

        yield return null;

        //Vector3 dir = event2Navi.transform.position - player.transform.position;

        //player.transform.position += dir.normalized * Time.deltaTime * 1;

        //yield return null;

        //if(Vector3.Distance(this.transform.position, event2Navi.transform.position) < 0.1f)
        //{
        //    StartCoroutine(Event2On());
        //}
        //else
        //{
        //    StartCoroutine(Event2Move());
        //}
    }
    
    IEnumerator Event2On()
    {
        bgmScript.PlayBGM(0, false);

        yield return new WaitForSeconds(5);
        bgmScript.PlayEffect(0, false);
        StartCoroutine(redLight());

        manequin1.SetActive(true);

        yield return new WaitForSeconds(4);
        bgmScript.PlayEffect(1, false);

        manequin1.SetActive(false);
        manequin2.SetActive(true);

        yield return new WaitForSeconds(2);

        StartCoroutine(FadeInOut(black, 2));

        yield return new WaitForSeconds(2);
        bgmScript.PlayEffect(2, false);
        manequin1.SetActive(false);
        manequin2.SetActive(false);
        manequin3.SetActive(true);
      

        bgmScript.PlayBGM(1, false);

        redLightRe = false;


        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeInOut(black, 20));

        yield return new WaitForSeconds(2);

        bgmScript.PlayEffect(3, false);

        yield return new WaitForSeconds(3);

        sceneloadScript.HallLoad();
    }

    IEnumerator redLight()
    {

        for (float i = 1f; i > 0.5f; i -= 0.05f)
        {
            dirLight.GetComponent<Light>().color = new Vector4(i, 0, 0, 1);
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        for (float i = 0.5f; i < 1; i += 0.05f)
        {
            dirLight.GetComponent<Light>().color = new Vector4(i, 0, 0, 1);
            yield return null;
        }

        if (redLightRe)
        {
            StartCoroutine(redLight());
        }
        else
        {
            dirLight.GetComponent<Light>().color = new Vector4(1, 0, 0, 1);
        }
    }

    //public IEnumerator FadeInOut(GameObject obj,float sec)
    //{
    //    // In이면 true, Out이면 fasle를 주면된다.
    //    // 노파심에 FadeIn이 투명->원본 점점 나타나는것
    //    // FadeOut이 원본->투명으로 점점 사라지는것.


    //    for (float i = 0; i <= 0.5f; i += 0.005f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<GUITexture>().color = color;
    //        yield return null;
    //    }
        
    //    yield return new WaitForSeconds(sec);

    //    for (float i = 0.5f; i >= 0; i -= 0.005f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<GUITexture>().color = color;
    //        yield return null;
    //    }

    //    obj.SetActive(false);
    //}

    //public IEnumerator TextFadeInOut(GameObject obj, float sec)
    //{
    //    // In이면 true, Out이면 fasle를 주면된다.
    //    // 노파심에 FadeIn이 투명->원본 점점 나타나는것
    //    // FadeOut이 원본->투명으로 점점 사라지는것.


    //    for (float i = 0; i <= 1f; i += 0.005f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<TextMesh>().color = color;
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(sec);

    //    for (float i = 1f; i >= 0; i -= 0.005f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<TextMesh>().color = color;
    //        yield return null;
    //    }

    //    obj.SetActive(false);
    //}

    public IEnumerator FadeInOut(GameObject obj, float sec)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.


        for (float i = 0; i <= 1f; i += 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }

        yield return new WaitForSeconds(sec);

        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }
    }

    IEnumerator FirstFade(GameObject obj)
    {
        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            // obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return 0;
        }
    }
}
