using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    // 도어 스크립트
    // 부모 태그로 판별하기 때문에 꼭 도어 손잡이에 태그를 넣어야한다.

    public bool open =  false;          // 열려있는지 아닌지   ture = 열린상태 fasle = 닫힌상태
    public bool doorLock = false;       // 잠긴상태인지 아닌지 ture = 잠김 false = 열림
    public bool reverse = false;        // 반대

    public AudioSource audio;           // 문 소리
    public AudioClip[] openclose;       // 열린소리와 닫히는 소리를 넣는 배열

    public HallEventScript eventScript;
    public HallGhostScript ghostScript;

    bool event3Active = false;

    //void Start()
    //{
    //    DoorActive(9, 120);
    //}

    // 문을 여는 코루틴을 활성화하는 함수
    public void DoorActive(int speed, int angle)
    {
        if (!doorLock)
        {
           if(reverse)
           {
               StartCoroutine(DoorOpen(this.gameObject, speed, angle * -1));
           }
           else
           {
               StartCoroutine(DoorOpen(this.gameObject, speed, angle));
           }
        }
        else
        {
            audio.clip = openclose[2];
            audio.Play();
        }
    }

    // 문을 잠금 상태로 바꿔주는 함수
    public void UnLock()
    {
        if(doorLock)
        {
            doorLock = false;
        }
    }

    // 문을 열린 상태로 바꿔주는 함수
    public void Lock()
    {
        if(!doorLock)
        {
            doorLock = true;
        }
    }

    // 문을 여는 코루틴
    IEnumerator DoorOpen(GameObject obj, int num, int angle)
    {
        int count = 0;                            // 몇번 돌렸는지
        float total = num * 10;                   // 최대 볓번 돌릴건지 (숫자가 높을 수록 빨라진다)

        float valueY = angle / num * 0.1f; ;      // y의 각도를 돌린다.

        // 문이 열렸으면 -1를 곱해줘서 반대로 회전 즉 닫아준다.
        if (open)
        {
            audio.clip = openclose[0];
            valueY *= -1;
            open = false;
        }
        else // 문이 닫힌경우 열어준다.
        {
            audio.clip = openclose[1];
            open = true;
        }
        // 문 열거나 닫는 소리 재생
        audio.Play();

        // 실질적으로 문의 각도를 변경하는 반복문.
        while (count < total)
        {
            yield return null;
            obj.transform.eulerAngles += new Vector3(0, valueY, 0);
            count++;
        }

        if (this.transform.name == "Event3Door" && !doorLock && !event3Active)
        {
            event3Active = true;
            eventScript.Event3();
        }
    }
}
