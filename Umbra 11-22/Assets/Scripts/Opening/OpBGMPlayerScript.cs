using UnityEngine;
using System.Collections;

public class OpBGMPlayerScript : MonoBehaviour {

    public AudioSource audio;
    public AudioClip[] BGMList;

    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
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

        if (fade)
        {
            audio.volume = 0.1f;

            float fadein = 1;

            while (audio.volume < fadein)
            {
                yield return new WaitForSeconds(0.5f);
                audio.volume += 0.05f;
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
