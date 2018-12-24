using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class r1EventScript : MonoBehaviour
{

    //
    /* 이 스크립트 상에서 모든 이벤트를 제어 할 것 */
    //
    public r1BGMPlayerScript bgmScript;
    public FadeScript fadeScript;
    public r1TextScript textScript;

    public GameObject[] lamps = new GameObject[1];

    // public GameObject chair;
    public GameObject paint;

    public Animator ghostAni;
    public Animator prameAni;

    public GameObject Player;
    public GameObject Mr_Ghost;
    public GameObject Pass;
    public GameObject Pass2;
    public GameObject memo;
    public GameObject follow;
    public Camera playerCamera;

    public GameObject hit1;
    public GameObject hit2;
    public GameObject hit3;
    public GameObject hit4;
    public GameObject sceanMove;

    public GameObject lampLight;
    //  public GameObject target;

    public float distance = 2.0f;
    public float smooth;
    public float speed = 2.5f;

    // 시간 이벤트를 위해 게임이 시작한 후 시간을 잰다.
    public float gameTime;

    public r1PhoneMessageScript r1messageScript;//간단한 힌트가 담긴 메세지


    // 일시정지 구상 중
    public bool isPause = false;

    //
    /* 이벤트 EX*/
    // 
    public bool Scene1 = false;       // 일단 논리값 하나 생성
    //public bool Opening2;             // ex


    public bool drop = false;

    public bool Chair = false;

    public bool doorLock = true;

    public bool ghostmove = false;

    public bool Idle;
    public bool walking;



    public GameObject[] lampTrap = new GameObject[5];
    public List<GameObject> lampAnswer = new List<GameObject>();

    public GameObject phone;

    //////////////////////////////
    //////////////////////////////
    // 강호현 씬네임 추가부분
    public SceneNameScript sceneScript;
    public SceneNameScript sceneScript2;
    public GameObject black;

    SceneManagerScript sceneloadScript;
    //////////////////////////////
    //////////////////////////////

    // 원하는 이벤트 만큼 추가 시킬것 

    void Start()
    {
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<r1BGMPlayerScript>();//BGN 스크립트 지정
        //fadeScript = GameObject.FindWithTag("GameController").GetComponent<FadeScript>();
        // textScript = GameObject.FindWithTag("GameController").GetComponent<r1TextScript>();       
        gameTime = 0;//게임 시작시 시간
        memo.SetActive(false);//메모 오브젝트의 상태를 비활성화
        playerCamera.GetComponent<Blur>().enabled = false;


        //////////////////////////////
        //////////////////////////////
        // 강호현 씬네임 추가부분
        sceneScript = GameObject.Find("SceneName").GetComponent<SceneNameScript>();
        sceneScript2 = GameObject.Find("SceneName2").GetComponent<SceneNameScript>();
        black = GameObject.Find("Black");
        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        //////////////////////////////
        //////////////////////////////



        //hit 1,2,3는 이벤트 트리거들이다. 
        hit1.SetActive(false);
        hit2.SetActive(false);
        hit3.SetActive(false);
        hit4.SetActive(false);

        //신전환 이벤트겸 갑툭튀 이벤트와 트리거들이다.
        follow.SetActive(false);

        sceanMove.SetActive(false);

        StartCoroutine(BGMEvent());


        StartCoroutine(R1PhoneEvent());


        //램프 키고 끄기용
        lampLight.SetActive(false);

        StartCoroutine(First());

        phone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    StartCoroutine(Trap());
        //}

        if (!isPause)
        {
            gameTime += Time.deltaTime;//게임타임이 흘러가는 시간의 증가
        }

        if (!drop && gameTime > 10)//방 진입 10초후 액자가 떨어진다.
        {
            paint.GetComponent<Rigidbody>().useGravity = true;//페인트의 리짓바디의 중력값을 참으로 한다.
            drop = true;//불형 이벤트를 참으로 돌려서 끝내도록 한다.                  
        }


        if (!ghostmove && Vector3.Distance(Pass.transform.position, Player.transform.position) > 8.0f)//두 오브젝트의 위치차가 10.5보다 클경우
        {
            StartCoroutine(GhostMoving());//귀신 이동 코루틴을 실행 해준다.

        }



        // if (!ghostmove && Vector3.Distance(Pass.transform.position, Player.transform.position) > 8.0f)//두 오브젝트의 위치차가 10.5보다 클경우
        //{

        //    Mr_Ghost.transform.Translate(Vector3.forward * speed * Time.deltaTime); //귀신이 지정해준 속도를 받아서 매시간 갱신하며 왼쪽으로 움직인다.
        //    ghostAni.SetBool("Walking", true);

        //    //Debug.Log("된다야!");

        //    lamps[0].GetComponent<r1LiteScript>().flicker();


        //    if (Vector3.Distance(Pass2.transform.position, Mr_Ghost.transform.position) > 10.5f)//두 오브젝트의 위치차가 10.5보다 클경우
        //    {
        //        Destroy(Mr_Ghost);//유령 오브젝트를 삭제한다.
        //        ghostAni.SetBool("Walking", false);
        //        memo.SetActive(true);//메모 오브젝트의 상태를 참으로 변경한다.
        //        ghostmove = true;//불형이벤트의 값을 참으로 바꿔 이벤트가 끝나게 한다.                        
        //    }
        //}
        // if (ghostmove)
        // {//이벤트 트리거들을 전부 작동시켜준다.
        //     hit1.SetActive(true);
        //     hit2.SetActive(true);
        //     hit3.SetActive(true);

        //  }

    }

    IEnumerator First()
    {
        yield return new WaitForSeconds(1);

        sceneScript.TextCopyStart("제 3 장", 3);

        yield return new WaitForSeconds(2);

        sceneScript2.TextCopyStart("봉인", 1);

        yield return new WaitForSeconds(5);

        StartCoroutine(FirstFade(black));

        phone.SetActive(true);
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

    IEnumerator GhostMoving()//귀신 이동 실행부분
    {

        Mr_Ghost.transform.Translate(Vector3.forward * speed * Time.deltaTime);//귀신의 좌표에 델타타임을 곱하여 실시간으로 이동시킨다.
        ghostAni.SetBool("Walking", true);//워킹 에니메이션을 사용

        lamps[0].GetComponent<r1LiteScript>().flicker();//전등 깜빡이는 함수를 호출해와 사용한다.

        if (Vector3.Distance(Pass2.transform.position, Mr_Ghost.transform.position) > 10.5f)//두 오브젝트의 위치차가 10.5보다 클경우(즉 귀신이 도착지점에 도달한 경우)
        {
            Destroy(Mr_Ghost);//유령 오브젝트를 삭제한다.
            ghostAni.SetBool("Walking", false);
            memo.SetActive(true);//메모 오브젝트의 상태를 참으로 변경한다.
            ghostmove = true;//불형이벤트의 값을 참으로 바꿔 이벤트가 끝나게 한다.

            //이벤트 트리거들을 전부 작동시켜준다.
            hit1.SetActive(true);
            hit2.SetActive(true);
            hit3.SetActive(true);
            hit4.SetActive(true);
        }
        yield return null;

    }

    IEnumerator BGMEvent()//배경은 재생 코루틴
    {
        yield return new WaitForSeconds(12);//12초후 재생되게 해준다.
        bgmScript.PlayBGM(0, true);//0번곡을 실행 시킨다.
    }





    // 메세지 코루틴
    IEnumerator R1PhoneEvent()
    {
        yield return new WaitForSeconds(3);
        //bgmScript.PlayEffect(0, false);       
        r1messageScript.ChangeMessage(1);        // 내가 설정한 첫번째 메시지 그림 넣기.
    }


    public void AddLamp(GameObject lamp)//램프킨 수를 저장해주는 부분
    {
        if (!lampAnswer.Contains(lamp))
        {
            lampAnswer.Add(lamp);

           StartCoroutine(Trap());//조건이 되었을 경우 코루틴 실행

        }

    }

    IEnumerator Trap()//함정 실행부분
    {
        int diffrent = 0;

        for (int i = 0; i < lampAnswer.Count; i++)//값이 증가되면
        {
            if (lampTrap[i].gameObject != lampAnswer[i].gameObject)//트랩과 정답을 비교해서 틀릴경우
            {
                diffrent++;//틀림값이 1증가 되게 해준다.
                //lampAnswer[i].GetComponent<r1LiteScript>().light.SetActive(false);
                break;
            }
            else
            {

            }
        }

            if (diffrent > 0)//틀림값이 0보다 클 경우
            {
                for (int i = 0; i < lampAnswer.Count; i++)//i의 최대값까지 증가 한다.
                {
                    if (lampAnswer[i].gameObject.GetComponent<r1LiteScript>().On)//해당 함수가 진행 되었을 경우
                    {
                        yield return new WaitForSeconds(0.2f);//0.2초마다 꺼지게 해준다.
                        lampAnswer[i].gameObject.GetComponent<r1LiteScript>().light.SetActive(false);//포인트 라이트작동 방지.
                        lampAnswer[i].gameObject.GetComponent<r1LiteScript>().LiteActive();//불 켜진부분을 꺼준다.
                        lampAnswer[i].gameObject.GetComponent<AudioSource>().Play();//사운드 실행
                    }
                }
                lampAnswer.Clear();//저장된 답 리스트를 지워준다.
            }
            else//정답일 경우
                if (lampAnswer.Count == 5)
                {
                    follow.SetActive(true);//신이동용 이벤트를 작동시킨다.
                    follow.GetComponent<r1follow>().Set = true;//이벤트의 움직임을 불러온다.
					follow.GetComponent<Animator>().SetBool("Running", true);
                    sceanMove.SetActive(true);//귀신이 내는 소리를 작동 시킨다.

                    yield return new WaitForSeconds(3);

                    sceneloadScript.BasementLoad();
                }
            yield return null;
        }
    }

    


 

