using UnityEngine;
using System.Collections;

public class r1LiteScript : MonoBehaviour
{
    //플레이어 오브젝트
   // public GameObject player;
    // 불빛 오브젝트
    public GameObject light;
    //받아올 이벤트 스크립트
    public r1EventScript r1eventScript;

   // public GameObject Outtrigger;
    //불 켜고 끄는 소리를 위해 선언
    public AudioSource audio;
    public AudioClip[] audioClip;

    //쉐이더 변경으로 램프의 on/off상태를 구분하기 쉽게 해준다.
    public Shader shader1;
    public Shader shader2;
    public Renderer rend;


    public bool On = false;//불값 형성
    //public bool onlamp = false;
    //public bool outdoor = false;

    void Start()
    {
        //audio = this.gameObject.GetComponent<AudioSource>();//오브젝트의 오디오 컴포넌트를 가져온다.

        //rend = GetComponent<Renderer>();//렌더러컴포넌트를 가져온다.
        //shader1 = Shader.Find("Standard");//쉐이더를 찾는다.
        //shader2 = Shader.Find("Unlit/Texture");//쉐이더를 찾는다.

    }

    public void LiteActive()//라이트 온오프 함수
    {
        StartCoroutine(LightOn());//함수가 실행되면 해당 코루틴을 돌린다.
    }

    //public void warong()
    //{
    //    StartCoroutine(LightOff());
    //}

    public void flicker()//라이트 점멸 함수
    {
        StartCoroutine(flicking());//전등이 점멸하게 해주는 코루틴 실행
    }

    //IEnumerator LightOff()
    //{

    //    if (On)
    //    {
    //        On = false;//선언의 값을 false로 바꿔준다.
    //        light.SetActive(false);//라이트를 꺼준다.
    //        audio.clip = audioClip[1];
    //        audio.Play();
    //        if (rend.material.shader == shader2)//쉐이더의 상태를 보고 바꿔준다.
    //        {
    //            rend.material.shader = shader1;
    //        }
    //    }
    //    yield return null;
    //}

    IEnumerator LightOn()//코루틴 시작.
    {

        if (On)//불형 선언이 작동 되었을 경우
        {
            //print("꺼지냐?");  
            On = false;//선언의 값이 false일 경우.
            //r1eventScript.AddLamp(this.gameObject);          
            audio.clip = audioClip[1];
            audio.Play();
            if (rend.material.shader == shader2)//쉐이더의 상태를 보고 바꿔준다.
            {
                rend.material.shader = shader1;
                //light.SetActive(true);
            }
            else
            {
                rend.material.shader = shader2;               
            }
            light.SetActive(false);//라이트를 꺼준다.  
        }
        else//아닐 경우
        {
            On = true;//불형 선언을 트루일 경우.
            r1eventScript.AddLamp(this.gameObject);
            light.SetActive(true);//라이트를 켜준다.
            audio.clip = audioClip[0];
            audio.Play();
            if (rend.material.shader == shader1)//쉐이더의 상태를 보고 바꿔준다.
            {
                rend.material.shader = shader2;
            }
            else
            {
                rend.material.shader = shader1;
                
            }   
        }
        yield return null;//반환 선언

    }

   IEnumerator flicking()//전등이 깜빡이게 해주는 부분
   {

       //라이트 온오프에 맞춰서 쉐이더들도 변경되게 만들어 줬다. 반짝이는건 yield return 값으로 조절

       light.SetActive(true);
       if (rend.material.shader == shader1)
       {
           rend.material.shader = shader2;
       }
       yield return new WaitForSeconds(0.5f);
              
       light.SetActive(false);
       if (rend.material.shader == shader2)
       {
           rend.material.shader = shader1;
       }
       yield return new WaitForSeconds(0.3f);

       light.SetActive(true);
       if (rend.material.shader == shader1)
       {
           rend.material.shader = shader2;
       }
       yield return new WaitForSeconds(0.2f);

       light.SetActive(false);
       if (rend.material.shader == shader2)
       {
           rend.material.shader = shader1;
       }
       yield return new WaitForSeconds(0.2f);

       light.SetActive(true);
       if (rend.material.shader == shader1)
       {
           rend.material.shader = shader2;
       }
       yield return new WaitForSeconds(0.3f);

       light.SetActive(false);
       if (rend.material.shader == shader2)
       {
           rend.material.shader = shader1;
       }

   }
}

    

    


 