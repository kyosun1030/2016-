using UnityEngine;
using System.Collections;

public class PrBGMPlayerScript : MonoBehaviour {

    public AudioSource audio;
    public AudioClip[] BGMList;

    public AudioSource audioVoice;
    public AudioClip[] VoiceList;

    public AudioSource backAudio;
    public AudioClip[] effectList;

    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }


    public void PlayEffect(int MusicNum, bool fade)
    {
        StartCoroutine(PlayerEffect(MusicNum, fade));
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

    public IEnumerator VoicePlayer(int MusicNum, bool fade) // 음악 번호, fade를 넣을지 안넣을지.
    {
        //if (audioVoice.isPlaying && fade)
        //{
        //    StartCoroutine(StopMusic(fade));
        //}

        audioVoice.clip = VoiceList[MusicNum]; // 어떤 음악을 넣을지?
        audioVoice.Play();   // 재생

        if (fade)
        {
            audioVoice.volume = 0.1f;

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
