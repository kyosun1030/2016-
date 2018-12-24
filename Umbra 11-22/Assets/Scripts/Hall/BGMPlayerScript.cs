using UnityEngine;
using System.Collections;

public class BGMPlayerScript : MonoBehaviour {

    public AudioSource audio;
    public AudioSource backAudio;
    public AudioClip[] BGMList;
    public AudioClip[] effectList;

    public void PlayBGM(int MusicNum, bool fade)
    {
        StartCoroutine(PlayerBGM(MusicNum, fade));
    }

    public void PlayEffect(int MusicNum, bool fade)
    {
        StartCoroutine(PlayerEffect(MusicNum, fade));
    }

    public void Stop(bool fade)
    {
        StartCoroutine(StopMusic(fade));
    }

    // 음악 플레이어 함수
    IEnumerator PlayerBGM(int MusicNum, bool fade) // 음악 번호, fade를 넣을지 안넣을지.
    {
        if(audio.isPlaying)
        {
            StartCoroutine(StopMusic(fade));
        }

        audio.clip = BGMList[MusicNum]; // 어떤 음악을 넣을지?
        audio.Play();   // 재생

        if (fade)
        {
            audio.volume = 0.1f;

            float fadein = 0.5f;

            while (audio.volume < fadein)
            {
                yield return new WaitForSeconds(0.5f);
                audio.volume += 0.01f;
            }
        }
    }

    IEnumerator PlayerEffect(int MusicNum, bool fade) // 음악 번호, fade를 넣을지 안넣을지.
    {
        //if (backAudio.isPlaying)
        //{
        //    StartCoroutine(StopMusic(fade));
        //}

        backAudio.clip = effectList[MusicNum]; // 어떤 음악을 넣을지?
        backAudio.Play();   // 재생

        if (fade)
        {
            backAudio.volume = 0.1f;

            float fadein = 1f;

            while (backAudio.volume < fadein)
            {
                yield return new WaitForSeconds(0.5f);
                backAudio.volume += 0.03f;
            }
        }
    }

    IEnumerator StopMusic(bool fade)
    {
        if (fade)
        {
            float fadeout = 0f;

            while (audio.volume > fadeout)
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
