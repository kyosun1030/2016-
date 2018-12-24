using UnityEngine;
using System.Collections;

public class PhoneMessageScript : MonoBehaviour
{
    public Texture[] allMessages;       // 바꿀 텍스쳐 배열
    public GameObject myPhone;

    public BGMPlayerScript bgmScript;

    void Start()
    {
        //bgmScript = GameObject.FindWithTag("GameController").GetComponent<BGMPlayerScript>();
    }


    // 메시지 변환 함수
    public void ChangeMessage(int num)      // 메시지 변환할 숫자
    {

            // 문자 메시지 효과음
        bgmScript.PlayEffect(2, false);

        //문자 메시지 화면이 켜져있지 않을 때만 실행되게.
        if (!myPhone.GetComponent<VRSmartPhoneScript>().messageDisplay.activeSelf)
        {
            myPhone.GetComponent<VRSmartPhoneScript>().messageNoticeBar.SetActive(true);
            myPhone.GetComponent<VRSmartPhoneScript>().messageNotice.SetActive(true);
        }

        // 현재 오브젝트의 렌더러 마테리얼 텍스쳐를 변경한다.
        this.GetComponent<Renderer>().material.SetTexture("_MainTex", allMessages[num - 1]);
    }
}