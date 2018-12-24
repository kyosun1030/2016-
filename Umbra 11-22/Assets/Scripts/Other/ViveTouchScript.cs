using UnityEngine;
using System.Collections;
using VRTK;

public class ViveTouchScript : MonoBehaviour {


    // 컨트롤러를 받아올 변수
    public VRTK_ControllerEvents rightController;

    // 이벤트 제어를 위한 스크립트
    public HallEventScript hallEventScript;

    // 어떤 것을 만지고 있는지 판별하는 변수
    public GameObject touchObject;

    // 한번만 실행하기 위해 판별값 하나 준다.
    public bool active = false;

    public Vector3 size;

	GameObject[] touchChild;

    void Update()
    {
        // 변수에 오브젝트가 있을 경우
        if(touchObject != null)
        {
            // 트리거 버튼을 누르고 있는경우 active 변수를 true로 준다.
            if(rightController.triggerClicked)
            {
                active = true;
            }
            // 그리고 버튼을 누르지 않을 때 이 조건문이 실행된다.
            else if(active)
            {
                Distinction();                          // 다양한 오브젝트의 명령을 하기 위해 판별 함수를 호출
                touchObject = null;   
                active = false;                         // 다시 실행 되지 않기 위해 active를 false로 바꿔준다.
            }
        }
    }

    // 빛나!
    IEnumerator Shine()
    {
        if (touchObject != null)
        {
			if(touchObject.GetComponent<Renderer>() != null)
			{
           	 for (float i = 0f; i < 0.5f; i += 0.01f)
           	 {
                Color color = new Vector4(i, i, i);
                touchObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
                yield return new WaitForEndOfFrame();
          	  }
			}
			else
			{
				for(int j = 0; j < touchChild.Length; j++)
				{
					for (float i = 0f; i < 0.5f; i += 0.01f)
					{
						Color color = new Vector4(i, i, i);
						touchChild[j].GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
						yield return new WaitForEndOfFrame();
					}			
				}
			}
        }

        yield return new WaitForEndOfFrame();

        if (touchObject != null)
        {
			if(touchObject.GetComponent<Renderer>() != null)
			{
            for (float i = 0.5f; i > 0f; i -= 0.01f)
            {
                Color color = new Vector4(i, i, i);
                touchObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
                yield return new WaitForEndOfFrame();
            }
			}
			else
			{
				for(int j = 0; j < touchChild.Length; j++)
				{
					for (float i = 0.5f; i > 0f; i -= 0.01f)
					{
						Color color = new Vector4(i, i, i);
						touchChild[j].GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
						yield return new WaitForEndOfFrame();
					}		
				}
			}
        }

        if (touchObject != null)
        {
            StartCoroutine(Shine());
        }
    }

    // 해당 오브젝트의 콜리더가 다른 콜리더와 접촉 했을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)            // 접촉한 콜리더 레이어가 9(Work)
        {
            if (touchObject == null)                // 터치 오브젝트가 비었다면
            {
                touchObject = other.gameObject;     // 접촉하고 있는 오브젝트를 넣어준다.                
                StartCoroutine(Shine());

               // size = touchObject.GetComponent<BoxCollider>().size;
            }
        }
    }

    // 해당 오브젝트의 콜리더와 다른 콜리더와 접촉 중 일 때
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)            // 접촉한 콜리더 레이어가 9(Work)
        {
            if (touchObject == null)                // 터치 오브젝트가 비었다면
            {
                touchObject = other.gameObject;     // 접촉하고 있는 오브젝트를 넣어준다.
                //touchObject.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.05f, 0.05f);

				if(touchObject.GetComponent<Renderer>() == null)
				{
					touchChild = GameObject.FindGameObjectsWithTag("Child");
				}
            }
        }
    }

    // 해당 오브젝트의 콜리더와 다른 콜리더와 벗어 났을 때
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)            // 접촉한 콜리더 레이어가 9(Work)
        {
            if (touchObject != null)                // 터치 오브젝트가 있다면
            {
                touchObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                touchObject = null;                 // 오브젝트 변수를 초기화.
                //touchObject.GetComponent<BoxCollider>().size = size;
            }
        }
    }

    //판별하는 함수
    void Distinction()
    {
        if (touchObject.name == "Phone")
        {
            hallEventScript.GetComponent<HallEventScript>().GetPhone();
        }
        else
        {
            switch (touchObject.tag)                     // 태그로 판별한다.
            {
                case "Door":                            // 태그가 Door라면
                    touchObject.GetComponentInParent<DoorScript>().DoorActive(9, 120);            // DoorScript에 함수를 호출 9,120이 평균적인 속도
                    break;
                case "Lamp":
                	touchObject.GetComponent<r1LiteScript>().LiteActive();
                	break;
                case "Drawer":
                    touchObject.GetComponent<DrawerScript>().DrawerActive();
                    break;
                case "Wing":
                    touchObject.GetComponent<WingScript>().WingActive(9, 120);
                    break;
            }
        }
    }
}

//Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;      //트리거
//Valve.VR.EVRButtonId tuch = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;        //터치패드

//public SteamVR_TrackedObject tobj;       //트렉오브젝트
//public SteamVR_Controller.Device controller
//{
//    get
//    {
//        return SteamVR_Controller.Input((int)tobj.index);
//    }
//}
//public bool left_trigger_down, left_trigger_stay, left_trigger_up;
//public bool left_tuch_down, left_tuch_stay;

//Transform grabbableObject;
//bool isGrabbing;

//// Use this for initialization
//void Start () {
//    tobj = GetComponent<SteamVR_TrackedObject>();   
//}

//// Update is called once per frame
//void Update () {
//    left_trigger_down = controller.GetPressDown(trigger);
//    left_trigger_stay = controller.GetPress(trigger);
//    left_trigger_up = controller.GetPressUp(trigger);
//    left_tuch_down = controller.GetPressDown(tuch);
//    left_tuch_stay = controller.GetPress(tuch);
//    //left커맨드
//}
