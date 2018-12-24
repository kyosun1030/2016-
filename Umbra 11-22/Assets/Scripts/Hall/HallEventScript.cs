using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class HallEventScript : MonoBehaviour {

    //
    /* 이 스크립트 상에서 모든 이벤트를 제어 할 것 */
    //
    public BGMPlayerScript bgmScript;
    public FadeScript fadeScript;
    public TextScript textScript;

    // 시간 이벤트를 위해 게임이 시작한 후 시간을 잰다.
    public float gameTime;

    // 일시정지 구상 중
    public bool isPause = false;

    //
    /* 스마트폰 습득 이벤트 */
    //
    public GameObject Phone;
    public GameObject myPhone;
    public DoorScript startDoor;
    //public bool getPhone = false;

    //
    /* 메시지 디스플레이 (메시지 변화)*/
    //
    public PhoneMessageScript pmessageScript;

    //
    /*시작*/
    //
    public Camera playerCamera;
    //public bool StartEvent = false;

    //
    /*룸1씬로드*/
    //
    public GameObject room1Load;

    //
    /*추격*/
    //
    public GameObject manGhost;
    //public bool manGhostEvent = false;

    //
    /*이벤트1*/
    // 
    public GameObject sc1Ghost;
    //public bool Event1 = false;       // 일단 논리값 하나 생성
    //public GameObject triggerEvent1;

    //
    /*이벤트2*/
    //
    public AudioSource asEvent2;
    public AudioClip acEvent2;
    public GameObject sopa;
    //public bool Event2 = false;
    //public GameObject triggerEvnet2;

    //
    /*이벤트3*/
    //
    public DoorScript event3Door;
    public AudioSource asEvent3;
    public AudioClip acEvent3;

    //
    /*이벤트4*/
    //
    public AudioSource asEvent4;
    public AudioClip acEvent4;

    //
    /*이벤트5*/
    //
    public GameObject headEvent5;

    //
    /*이벤트6*/
    //
    public GameObject runGhost;
    public GameObject youngGirl;

    // 기타 // 
    public GameObject Player;

    public float distancetest;

    public SceneNameScript sceneScript;
    public SceneNameScript sceneScript2;
    //public SceneNameScript objective;
    //public string[] objectives;
    public GameObject black;

    public GameObject way1;
    public GameObject way2;
    public GameObject way3;
    public GameObject way4;


    // 원하는 이벤트 만큼 추가 시킬것 
    void Start()
    {
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<BGMPlayerScript>();
        textScript = GameObject.FindWithTag("GameController").GetComponent<TextScript>();
        sceneScript = GameObject.Find("SceneName").GetComponent<SceneNameScript>();
        sceneScript2 = GameObject.Find("SceneName2").GetComponent<SceneNameScript>();
        //objective = GameObject.Find("Objective").GetComponent<SceneNameScript>();
        black = GameObject.Find("Black");

		playerCamera.GetComponent<Blur> ().enabled = false;

        // pmessageScript = GameObject.FindWithTag("Phone").GetComponentInChildren<PhoneMessageScript>();

        // 씬이 시작된후 흐른 시간
        gameTime = 0;

        // 씬 시작 코루틴 이벤트 호출
        StartCoroutine(First());

    }


    IEnumerator First()
    {
        yield return new WaitForSeconds(1);

        sceneScript.TextCopyStart("제 1 장", 3);

        yield return new WaitForSeconds(2);

        sceneScript2.TextCopyStart("낯선자", 1);

        yield return new WaitForSeconds(5);

        StartCoroutine(FirstFade(black));

        //yield return new WaitForSeconds(1);

        StartCoroutine(StartSceneEvent());
    }


    ///////////////////////////
    ///////////////////////////

    // 처음 씬 시작 코루틴
    IEnumerator StartSceneEvent()
    {
        // 카메라를 레이어 변경 (1 == Defalut)
        //playerCamera.cullingMask |= 9;//LayerMask.GetMask("Defalut");
        //playerCamera.cullingMask |= 9;

		playerCamera.GetComponent<Blur> ().enabled = true;

		playerCamera.GetComponent<Blur>().iterations = 10;
		playerCamera.GetComponent<Blur>().blurSpread = 0.6f;

        yield return new WaitForSeconds(2);

        StartCoroutine(BlurSet(8, -1));

        yield return new WaitForSeconds(1);

        StartCoroutine(BlurSet(4, 1));

        yield return new WaitForSeconds(2);

        StartCoroutine(BlurSet(6, -1));

        yield return new WaitForSeconds(3);

        playerCamera.GetComponent<Blur>().enabled = false;

       // objective.TextCopyStart("방에서 나갈 방법을 찾아보자.", 3);
    }

    // 블러 조작
    IEnumerator BlurSet(int count, int add)
    {
        int temp = 0;
        while (temp < count)
        {
            playerCamera.GetComponent<Blur>().iterations += add;
            temp++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    ///////////////////////////
    ///////////////////////////

    public void GetPhone()
    {
        StartCoroutine(GetPhoneEvent());
    }

    // 핸드폰 습득 코루틴
    IEnumerator GetPhoneEvent()
    {
        bgmScript.PlayEffect(0, false);         // 무언가를 습득했을 효과음
        Phone.SetActive(false);					// 바닥에 떨어진 폰 비활성화
        myPhone.SetActive(true);				// 폰 활성화		

        yield return new WaitForSeconds(2);

        pmessageScript.ChangeMessage(1);        // 내가 설정한 첫번째 메시지 그림 넣기.

        yield return new WaitForSeconds(2);

        pmessageScript.ChangeMessage(2);        //  내가 설정한 두번째 메시지 그림 넣기.

        yield return new WaitForSeconds(3);

        startDoor.doorLock = false;            // 문의 잠금상태를 열림으로
        startDoor.DoorActive(9, 120);

       // objective.TextCopyStart("문이 열린거 같다.", 4);
    }

    ///////////////////////////
    ///////////////////////////

    // 이벤트1 실행 함수
    // 이벤트1 내용 : 남자 유령이 문으로 사라진다.
    public void Event1()
    {
        StartCoroutine(SceneEvent1());
    }

    // 첫번째 씬 이벤트 
    IEnumerator SceneEvent1()
    {
        way1.SetActive(true);

        sc1Ghost.SetActive(true);

        // 남자 유령이 사라질 위치 설정
        Vector3 location = new Vector3(sc1Ghost.transform.position.x - 3.5f, sc1Ghost.transform.position.y, sc1Ghost.transform.position.z);

        // 배열 1번 효과음 재생
        bgmScript.PlayEffect(1, false);

        // 걷는 애니메이션을 위한 애니메이터 논리값 설정
        sc1Ghost.GetComponent<Animator>().SetBool("Walking", true);


        // 실질적으로 움직는 코드
        while (Vector3.Distance(sc1Ghost.transform.position, location) > 0.1f)
        {
            yield return null;
            sc1Ghost.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
        }


        // 배열 0번째 배경음악 재생
        bgmScript.PlayBGM(0, true);

        yield return new WaitForSeconds(1);

        // 서있는 상태로 만들기 위한 애니메이터 논리값 설정
        sc1Ghost.GetComponent<Animator>().SetBool("Walking", false);

        sc1Ghost.transform.eulerAngles = new Vector3(0, 90, 0);

        sc1Ghost.SetActive(false);
    }

    ///////////////////////////
    ///////////////////////////

    // 이벤트2 실행 함수
    // 이벤트2 내용 : 창문을 두드린 후 여자의 웃음소리가 들린다..
    public void Event2()
    {
        StartCoroutine(SceneEvent2());
    }

    IEnumerator SceneEvent2()
    {
        way1.SetActive(false);
        way2.SetActive(true);


        asEvent2.clip = acEvent2;
        asEvent2.Play();

        if (sopa.activeSelf)
        {
            sopa.SetActive(false);
        }

        yield return new WaitForSeconds(8);
        
        asEvent2.Stop();
    }

    ///////////////////////////
    ///////////////////////////

    // 이벤트3 실행 함수
    // 이벤트3 내용 : 문을 열자말자 남자의 목소리가 들리면 문이 빠르게 닫힌다.
    public void Event3()
    {
        StartCoroutine(SceneEvent3());
    }

    IEnumerator SceneEvent3()
    {
        yield return new WaitForSeconds(1.5f);

        sc1Ghost.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        event3Door.DoorActive(2, 120);

        yield return new WaitForSeconds(0.4f);

        asEvent3.clip = acEvent3;
        asEvent3.Play();

        event3Door.doorLock = true;

        yield return new WaitForSeconds(4);

        pmessageScript.ChangeMessage(3);

        sc1Ghost.SetActive(false);
    }

    ///////////////////////////
    ///////////////////////////
    // 이벤트4 실행 함수
    // 이벤트4 내용 : 고양이 울음 소리가 들린다.
    public void Event4()
    {
        StartCoroutine(SceneEvent4());
    }

    IEnumerator SceneEvent4()
    {
        asEvent4.clip = acEvent4;
        asEvent4.Play();

        yield return new WaitForSeconds(4);
        asEvent4.Stop();
    }

    ///////////////////////////
    ///////////////////////////

    ///////////////////////////
    ///////////////////////////

    // 이벤트5 실행 함수
    // 이벤트5 내용 : 천장에서 마네킹 머리가 떨어진다.
    public void Event5()
    {
        StartCoroutine(SceneEvent5());
    }

    IEnumerator SceneEvent5()
    {
        way2.SetActive(false);
        way3.SetActive(true);

        bgmScript.Stop(false);

        headEvent5.SetActive(true);
        bgmScript.PlayEffect(3, false);

        yield return new WaitForSeconds(2);

        bgmScript.PlayBGM(1, true);
    }

    ///////////////////////////
    ///////////////////////////
    ///////////////////////////
    ///////////////////////////

    ///////////////////////////
    ///////////////////////////

    // 이벤트6 실행 함수
    // 이벤트6 내용 : 
    public void Event6()
    {
        StartCoroutine(SceneEvent6());
    }

    IEnumerator SceneEvent6()
    {
        way3.SetActive(false);
        way4.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        runGhost.SetActive(true);
        runGhost.GetComponent<Animator>().SetBool("Running", true);
        runGhost.GetComponent<AudioSource>().Play();

        Vector3 goal = new Vector3(runGhost.transform.position.x,runGhost.transform.position.y, runGhost.transform.position.z + 30);

        while (Vector3.Distance(runGhost.transform.position, goal) > 0.1f)
        {
            yield return null;
            runGhost.transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 10;
        }

        yield return new WaitForSeconds(1);

        runGhost.SetActive(false);

        yield return new WaitForSeconds(1);

        youngGirl.SetActive(true);

        yield return new WaitForSeconds(1);

        //objective.TextCopyStart("누군가의 콧소리가 들린다.", 4);
       //pmessageScript.ChangeMessage(4);
    }

    IEnumerator FirstFade(GameObject obj)
    {
        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            //obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return 0;
        }
    }

    ///////////////////////////
    ///////////////////////////

}