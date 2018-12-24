using UnityEngine;
using System.Collections;

//트리거 충돌시 사운드가 나게해주는 부분이다.
public class r1TriggerSound : MonoBehaviour {

    //관련 된 것들 선언
    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource au;
    public bool alreadyPlayed = false;

	// Use this for initialization
	void Start () {
        au = GetComponent<AudioSource>();//au 에 오디오 컴포넌트를 넣어준다.
	
	}
	
	// Update is called once per frame
    //트리거 충돌시 실행되어 준다.
	void OnTriggerEnter () {
	if(!alreadyPlayed)
    {
        au.PlayOneShot(SoundToPlay, Volume);//어떤 사운드를 플레이 할건지, 볼륨 값을 얼마로 조정할것인지.
        alreadyPlayed = true;//불값을 바꿔서 한번만 작동되게 해준다.
    }
    }
}
