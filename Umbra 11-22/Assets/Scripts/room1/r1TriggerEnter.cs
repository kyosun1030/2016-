using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class r1TriggerEnter : MonoBehaviour {
    

    // 플레이어 & 이벤트 스크립트
    
    //public GameObject bed;
    public Camera playerCamera;//오브젝트 선언
    // Use this for initialization


    public bool alreadyPlayed = false;//불형 선언

	void OnTriggerEnter()//트리거 엔터 선언
    {
        if (!alreadyPlayed)//불형상태가 조건이다.
        {
            playerCamera.GetComponent<Blur>().enabled = true;//블러 효과가 되게 설정
            StartCoroutine(StartSceneEvent());//코루틴 실행
			alreadyPlayed = true;//한번 실행시키고 멈추기 위해 트루로 고정
        }

	}

   
    IEnumerator StartSceneEvent()//코루틴 실행
    {
        // 카메라를 레이어 변경 (1 == Defalut)
        //playerCamera.cullingMask |= 9;//LayerMask.GetMask("Defalut");
        //playerCamera.cullingMask |= 9;

        yield return new WaitForSeconds(2);

        StartCoroutine(BlurSet(4, -1));//블러효과를 주는 부분이다.

        yield return new WaitForSeconds(1);
        

        playerCamera.GetComponent<Blur>().enabled = false;//위에서 리턴이 끝나면 정상화 시켜준다.
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

    
	// Update is called once per frame
	void Update () {
	
	}
}
