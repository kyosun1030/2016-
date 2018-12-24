using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class r1Hit2Enter : MonoBehaviour {
    

    // 플레이어 & 이벤트 스크립트

    public GameObject bed;//움직이게 할 물체 설정


    public bool alreadyPlayed1 = false;

	void OnTriggerEnter()
    {
        if (!alreadyPlayed1)//해당 조건일 경우
        {
            
            StartCoroutine(bedMove());//코루틴 실행
            alreadyPlayed1 = true;//불값을 true로 변경
        }

	}

    IEnumerator bedMove()//코루틴 실행
    {
        Vector3 location = new Vector3(bed.transform.position.x-0.8f, bed.transform.position.y, bed.transform.position.z);//테이블이 움직일 위치 설정

        while (Vector3.Distance(bed.transform.position, location) > 0.1f)//실질적인 움직임 수행
        {
            yield return null;
            bed.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;//방향 으로 움직인다.

        }
       
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

    
	// Update is called once per frame
	void Update () {
	
	}
}
