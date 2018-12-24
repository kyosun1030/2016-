using UnityEngine;
using System.Collections;

//유령이 따라오는 이벤트 스크립트
public class r1follow : MonoBehaviour {

    //오브젝트 선언
    public Transform target;

    public float distance;
	
	public Animator ghostAni;

    //움직이는 속도 설정.
    float speed = 10f;

    public bool Set = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if(Set)
        {
            follow();//조건에 맞으면 함수가 실행된다.
        }
	}

    public void follow()//따라오는 함수
    {
        distance = Vector3.Distance(target.transform.position, this.transform.position);//타겟과 객체의 포지션을 받아온다.
        if (distance > 0.3f)//만약 해당 플롯값 이상으로 차이가 난다면
        {
            Vector3 dir = target.transform.position - this.transform.position;//차이 값을 계산한다.
            this.transform.position += dir.normalized * speed * Time.deltaTime;//차이만큼 값에 선언해준 속도를 곱해준다. 델타타임으로 계속 갱신되면서 움직이게 해준다.
            transform.LookAt(target.transform.position);//귀신의 보는 방향을 설정 해준다.
        }


        ghostAni.SetBool("Idle", true);//애니메이션 실행
    }
}
