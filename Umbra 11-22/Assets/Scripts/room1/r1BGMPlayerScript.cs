using UnityEngine;
using System.Collections;
//BGM관련 스크립트.
public class r1BGMPlayerScript : MonoBehaviour {

    //오디오 관련 함수들 선언
    public AudioSource audio;
    public AudioSource backAudio;
    public AudioClip[] BGMList;
    public AudioClip[] effectList;


    public void PlayBGM(int MusicNum, bool fade)//음악 실행 관련으로 코루틴 실행을 위해 선언
    {
        StartCoroutine(Player(MusicNum, fade));
    }
    public void PlayEffect(int MusicNum, bool fade)//이펙트 음악을 실행하기 위해 선언
    {
        StartCoroutine(PlayerEffect(MusicNum, fade));
    }

    public void Stop(bool fade)//음악을 중지 시키기 위해 선언. fade는 서서히 증가나 감소를 사용할지를 묻는것이다. true면 사용, false면 미사용.
    {
        StartCoroutine(StopMusic(fade));
    }
   
    // 음악 플레이어 함수
    public IEnumerator Player(int MusicNum, bool fade) // 음악 번호, fade를 넣을지 안넣을지.
    {
        if(audio.isPlaying && fade)
        {
            StartCoroutine(StopMusic(fade));
        }

        audio.clip = BGMList[MusicNum]; // 어떤 음악을 넣을지?
        audio.Play();   // 재생

        if (fade)//페이드가 true 일때 실행된다.
        {
            audio.volume = 0.1f;//오디오 볼륨 초기값을 설정

            float fadein = 0.15f;//페이드 사용시 증가되는 볼륨값

            while (audio.volume < fadein)
            {
                yield return new WaitForSeconds(0.5f);//0.5초마다 작동하며
                audio.volume += 0.05f;//해당 값만큼 볼륨이 계속 증가한다.
            }
        }
    }

    //이펙트 음 플레이
    IEnumerator PlayerEffect(int MusicNum, bool fade) // 음악 번호, fade를 넣을지 안넣을지.
    {
        if (backAudio.isPlaying)
        {
            StartCoroutine(StopMusic(fade));
        }

        backAudio.clip = effectList[MusicNum]; // 어떤 음악을 넣을지?
        backAudio.Play();   // 재생

        if (fade)
        {
            backAudio.volume = 0.1f;

            float fadein = 0.3f;

            while (backAudio.volume < fadein)
            {
                yield return new WaitForSeconds(0.5f);
                backAudio.volume += 0.03f;
            }
        }
    }

    public IEnumerator StopMusic(bool fade)
    {
        if (fade)
        {
            float fadeout = 0;

            while (audio.volume < fadeout)
            {
                yield return new WaitForSeconds(0.5f);
                audio.volume -= 0.1f;
            }
        }

        else
        {
            audio.Stop();
        }
    }
}
