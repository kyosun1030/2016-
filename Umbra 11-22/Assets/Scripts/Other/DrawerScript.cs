using UnityEngine;
using System.Collections;

public class DrawerScript : MonoBehaviour {

    public bool open = false;          // 열려있는지 아닌지   ture = 열린상태 fasle = 닫힌상태
    public bool drawerLock = false;       // 잠긴상태인지 아닌지 ture = 잠김 false = 열림
//    public bool reverse = false;        // 반대

    public AudioSource audio;           // 문 소리
    public AudioClip[] openclose;       // 열린소리와 닫히는 소리를 넣는 배열

    //public HallEventScript eventScript;

    void Start()
    {
        audio = this.transform.GetComponent<AudioSource>();
        //DrawerActive();
    }

    public void DrawerActive()
    {
        if (!open)
        {
            audio.clip = openclose[0];
            audio.Play();
            StartCoroutine( DrawerOpen(-1, 6));
            open = true;
        }
        else
        {
            audio.clip = openclose[0];
            audio.Play();
             StartCoroutine( DrawerOpen(1, 6));
             open = false;
        }
    }

    IEnumerator DrawerOpen(int dir, int num)
    {
        int count = 0;                            // 몇번 돌렸는지
        float total = num * 10;                   // 최대 볓번 돌릴건지 (숫자가 높을 수록 빨라진다)

        while (count < total)
        {
            yield return null;
            this.transform.localPosition += new Vector3(0.01f * dir, 0, 0);
            count++;
        }
    }


    // 문을 여는 코루틴
    IEnumerator WingOpen(GameObject obj, int num, int angle)
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

        //if (this.transform.name == "Event3Door" && !wingLock)
        //{
        //    eventScript.Event3();
        //}
    }
}
