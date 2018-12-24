using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using VRTK;

public class PrEventScript : MonoBehaviour {

    //
    /* 이 스크립트 상에서 모든 이벤트를 제어 할 것 */
    //
    public PrBGMPlayerScript bgmScript;
    //public FadeScript fadeScript;
    public GameObject player;

    // 시간 이벤트를 위해 게임이 시작한 후 시간을 잰다.
    public float gameTime;

    // 일시정지 구상 중
    public bool isPause = false;

    //
    /* 이벤트1*/
    // 
    public bool Event1 = false;       // 일단 논리값 하나 생성
    
    // 오프닝 이미지 1,2,3
    public GameObject image1;         
    public GameObject image2;
    public GameObject image3;
    
    // 블랙 스크린
    public GameObject black;
    public GameObject logo;
    public GameObject nextLoad;

    public SceneManagerScript sceneloadScript;

    //
    /* 튜토리얼*/
    // 
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

    public VRTK_TouchpadWalking touchWalkScript;

    void Start()
    {
        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<PrBGMPlayerScript>();

        black.GetComponent<Renderer>().material.color = new Vector4(1,1,1,0);

        gameTime = 0;

        touchWalkScript.maxWalkSpeed = 0;

        bgmScript.StartCoroutine(bgmScript.Player(0, false));

		StartCoroutine (Tutorial ());
        //StartCoroutine(Prologue());
    }

    public void NextScene()
    {
        StartCoroutine(Prologue());
    }

    IEnumerator Prologue()
    {
        player.transform.position = new Vector3(0, 1, -6);
        
        StartCoroutine(ObjFadeInOut(black, 60));
        yield return new WaitForSeconds(3f);
        StartCoroutine(ObjFadeInOut(image1, 3));
        yield return new WaitForSeconds(6);
        StartCoroutine(ObjFadeInOut(image2, 8));
        bgmScript.StartCoroutine(bgmScript.VoicePlayer(0, false));
        yield return new WaitForSeconds(4);
        StartCoroutine(ObjFadeInOut(image3, 4));
        bgmScript.StartCoroutine(bgmScript.VoicePlayer(1, false));
        yield return new WaitForSeconds(6);
        StartCoroutine(ObjFadeInOut(logo, 4.5f));
        yield return new WaitForSeconds(6);

        sceneloadScript.HallLoad();
        //async.allowSceneActivation = true;
        //StartCoroutine(Move());
    }

    IEnumerator Tutorial()
    {
        if (messageApp.activeSelf && !messageAppOn)
        {
            messageAppOn = true;
            messageAppActive.GetComponent<TextMesh>().text = "메시지 1/1";
            bgmScript.PlayEffect(0, false);
        }
        else if (naviApp.activeSelf && !naviAppOn)
        {
            naviAppOn = true;
            naviAppActive.GetComponent<TextMesh>().text = "네비게이션 1/1";
            bgmScript.PlayEffect(0, false);
        }
        else if (cameraApp.activeSelf && !cameraAppOn)
        {
            cameraAppOn = true;
            cameraAppActive.GetComponent<TextMesh>().text = "카메라 1/1";
            bgmScript.PlayEffect(0, false);
        }

        yield return null;

        if (messageAppOn && naviAppOn && cameraAppOn)
        {
            for (int i = 0; i < tips.Length - 1; i++)
            {
                tips[i].SetActive(false);
            }

            yield return new WaitForSeconds(1);

            gamestart.SetActive(true);

            yield return new WaitForSeconds(4);

            gamestart.SetActive(false);

            touchWalkScript.maxWalkSpeed = 5;
        }
        else
        {
            StartCoroutine(Tutorial());
        }
    }

    public IEnumerator FadeInOut(GameObject obj,float sec)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.

        for (float i = 0; i <= 0.5f; i += 0.005f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<GUITexture>().color = color;
            yield return null;
        }
        
        yield return new WaitForSeconds(sec);

        for (float i = 0.5f; i >= 0; i -= 0.005f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<GUITexture>().color = color;
            yield return null;
        }

        obj.SetActive(false);
    }

    public IEnumerator TextFadeInOut(GameObject obj, float sec)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.


        for (float i = 0; i <= 1f; i += 0.005f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<TextMesh>().color = color;
            yield return null;
        }

        yield return new WaitForSeconds(sec);

        for (float i = 1f; i >= 0; i -= 0.005f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<TextMesh>().color = color;
            yield return null;
        }

        obj.SetActive(false);
    }

    public IEnumerator ObjFadeInOut(GameObject obj, float sec)
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

    IEnumerator CanvasObjFadeInOut(GameObject obj, float sec)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.


        for (float i = 0; i <= 1f; i += 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return 0;
        }

        yield return new WaitForSeconds(sec);

        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return 0;
        }
    }
}
