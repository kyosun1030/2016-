using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class r1Hit4Enter : MonoBehaviour {


    // 플레이어 & 이벤트 스크립트

    public GameObject frame;//움직이게 할 물체 설정
    public r1BGMPlayerScript bgmScript;
    public bool alreadyPlayed1 = false;

   
   // public bool wait;         // 열려있는지 아닌지   ture = 열린상태 fasle = 닫힌상태    

    //public bool rotate;
    
    void OnTriggerEnter()
    {
        if (!alreadyPlayed1)//해당 조건일 경우
        {


            StartCoroutine(FrameRotating(frame.gameObject, 18.0f, 360));
            alreadyPlayed1 = true;//불값을 true로 변경
        }

    }

    //void Start()
    //{
    //    print("스타트");
    //    StartCoroutine(FrameRotating(frame, 18.0f, 360));
    //}

    
    IEnumerator FrameInComing()//코루틴 실행
    {
        
        Vector3 location = new Vector3(frame.transform.position.x - 30.0f, frame.transform.position.y, frame.transform.position.z);//테이블이 움직일 위치 설정
        
        yield return new WaitForSeconds(3.5f);
        bgmScript.PlayEffect(1, false);
       
        while (Vector3.Distance(frame.transform.position, location) > 0.1f)//실질적인 움직임 수행
        {
            yield return null;// new WaitForSeconds(2);
            frame.transform.position += new Vector3(-15, 0, 0) * Time.deltaTime*1.5f;//방향 으로 움직인다.
           if (Vector3.Distance(frame.transform.position, location) > 30.1f)//프레임이 위치값만큼 벌어질 경우
           {
               Destroy(frame);//오브젝트를 날려버린다.
           }
        }

    }

    IEnumerator FrameRotating(GameObject obj, float num, int angle)
    {
        int count = 0;                            // 몇번 돌렸는지
        float total = num * 5;                   // 최대 볓번 돌릴건지 (숫자가 높을 수록 빨라진다)

        float valueX = -1f; ;      // y의 각도를 돌린다.

        //float valueX = 1f;

        // 문이 열렸으면 -1를 곱해줘서 반대로 회전 즉 닫아준다.
        //if (rotate)
        //{
        //    valueX *= -1;
        //    rotate = false;
        //}
        // 실질적으로 문의 각도를 변경하는 반복문.
        while (count < total)
        {
            yield return null;
            obj.transform.eulerAngles += new Vector3(valueX, 0, 0);
            count++;
        }

        StartCoroutine(FrameInComing());//코루틴 실행
    }
    // Update is called once per frame
    void Update()
    {

    }
}
