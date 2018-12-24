using UnityEngine;
using System.Collections;

public class LampScript : MonoBehaviour {

    // 주인공 오브젝트
    public GameObject player;
    // 불빛 오브젝트
    public GameObject light;

    //어디오, 그리고 효과음
    public AudioSource audio;
    public AudioClip effect;

    // 한번만 실행 하기 위한 판별값
    public bool on = false;
	
    public void Active()
    {
        if (!on)
        {
            StartCoroutine(LightOn());
            on = true;
        }
    }


    // 전등이 꺼졌다 켜졌다 한다.
    public IEnumerator LightOn()
    {
        audio.clip = effect;
        audio.Play();

        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        light.SetActive(true);
        yield return new WaitForSeconds(1f);
        light.SetActive(false);
    }
}

// Update is called once per frame
//void Update () {
//    if(on)
//    {
//        StartCoroutine(LightOn());
//        on = false;
//    }
//}
