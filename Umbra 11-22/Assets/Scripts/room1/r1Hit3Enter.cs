using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class r1Hit3Enter : MonoBehaviour {
    

    // 플레이어 & 이벤트 스크립트
   

    //public Camera playerCamera;
    // Use this for initialization

    //오브젝트등을 선언
    public GameObject blowup;
    public GameObject trigger2;
    public Animation anim;
    public bool alreadyPlayed2 = false;

	void OnTriggerEnter()//트리거 발동
    {
        if (!alreadyPlayed2)//해당 불값일 경우
        {
           
            StartCoroutine(TableUp());// 유령효과 코루틴을 실행시킨다.
            
            trigger2.SetActive(true);//트리거르 ㄹ발동 시킨다.
          
            alreadyPlayed2 = true;//불값을 변경 시킨다.
        }

	}

    //void Update()
    //{
    //    anim = GetComponent<Animation>();
    //    foreach (AnimationState state in anim)
    //    {
    //        state.speed = 0.5F;
    //    }
    //}

    IEnumerator TableUp()//코루틴 수행
    {
        Vector3 location = new Vector3(blowup.transform.position.x, blowup.transform.position.y+1.5f, blowup.transform.position.z);//테이블이 움직일 위치 설정

        while (Vector3.Distance(blowup.transform.position, location) > 0.1f)//실질적인 움직임 수행
        {
            yield return new WaitForSeconds(0.05f); //반환값 0.03초
            blowup.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;//움직임 방향 설정

        }
        yield return new WaitForSeconds(0.75f);//체공시간
        blowup.GetComponent<Rigidbody>().useGravity = true;//중력값 설정
    }
    //IEnumerator StartSceneEvent()
    //{
    //    // 카메라를 레이어 변경 (1 == Defalut)
    //    //playerCamera.cullingMask |= 9;//LayerMask.GetMask("Defalut");
    //    //playerCamera.cullingMask |= 9;

    //    yield return new WaitForSeconds(2);

    //    StartCoroutine(BlurSet(8, -1));

    //    yield return new WaitForSeconds(1);

    //    StartCoroutine(BlurSet(4, 1));      

    //    playerCamera.GetComponent<Blur>().enabled = false;
    //}

    //// 블러 조작
    //IEnumerator BlurSet(int count, int add)
    //{
    //    int temp = 0;
    //    while (temp < count)
    //    {
    //        playerCamera.GetComponent<Blur>().iterations += add;
    //        temp++;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}

  
   

}

