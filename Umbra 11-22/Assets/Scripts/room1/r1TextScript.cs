using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class r1TextScript : MonoBehaviour {

    public GameObject Canvas; // 캔버스에 넣기 위해 꼭 넣어줘야된다.

    public Text font;

    public float width = Screen.width;
    public float height = Screen.height;

    //
    /* 예시 */
    //

    //void Start()
    //{
    //    // 내가 출력하고 싶은 문자열, 가로 위치, 세로 위치, 문자가 나타낼 시간, 폰트크기,색깔
    //    //                              0.5f면 중앙 쯤                          
    //    StartCoroutine(FadeText("안녕하세요", Screen.width * 0.5f, Screen.height * 0.5f, 2, 30, Color.red));
    //}


    //
    /* TextFade*/
    //

    public IEnumerator FadeText(string text, float x, float y, float delay, int Size, Color setColor)
    {
        //
        /* Text 생성 부분 */
        //

        GameObject temp = new GameObject();                                                          // 게임오브젝트 동적 생성
        temp.transform.SetParent(Canvas.transform);                                                  // 부모 설정
        Text printtext = temp.AddComponent<Text>();                                                  // 컴포넌트 추가
        printtext.font = font.font;                                                                  // 폰트 설정
        printtext.color = setColor;                                                                  // 폰트 색상
        printtext.fontSize = Screen.height / 100 + Size;                                             // 폰트 크기 설정
        // 공식은 높이에서 100을 Size를 더한다.
        printtext.rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height * 0.1f);     // 텍스트 박스 크기 설정
        printtext.rectTransform.position = new Vector2(x, y);                                        // 텍스트 박스 위치 설정

        int lengthmax = 0;                                                                           // 최대 문자길이 판별하는 변수

        //
        /* Text 한글자씩 출력 부분 */
        //

        while (lengthmax <= text.Length)
        {
            yield return new WaitForSeconds(0.05f);                                                  // 한글자씩 생성되는 주기

            string copy = text.Substring(0, lengthmax);                                              // 한글자씩 출력하기 위해.

            printtext.GetComponent<Text>().text = copy;                                              // 문자 입력
            lengthmax++;                                                                             // 문자 위치 변경 
        }


        //
        /* Fade 부분 */
        //
        yield return new WaitForSeconds(delay);                                                      // 문자를 몇초간 나타낼지                         

        for (float i = 1f; i >= 0; i -= 0.01f)                                                        // Fade 반복문                                     
        {
            printtext.GetComponent<Text>().color = new Color(printtext.color.r, printtext.color.g, printtext.color.b, i);
            yield return 0;
        }

        yield return new WaitForSeconds(1);

        Destroy(printtext);                                                                          // 텍스트 오브젝트 삭제.
    }

    public IEnumerator NormalText(string text, float x, float y, float delay, int Size, Color setColor)
    {
        GameObject temp = new GameObject();                                                          // 게임오브젝트 동적 생성
        temp.transform.SetParent(Canvas.transform);                                                  // 부모 설정
        Text printtext = temp.AddComponent<Text>();                                                  // 컴포넌트 추가
        printtext.font = font.font;                                                                  // 폰트 설정
        printtext.color = setColor;                                                                  // 폰트 색상
        printtext.fontSize = Screen.height / 100 + Size;                                             // 폰트 크기 설정
        // 공식은 높이에서 100을 Size를 더한다.
        printtext.rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height * 0.1f);     // 텍스트 박스 크기 설정
        printtext.rectTransform.position = new Vector2(x, y);                                        // 텍스트 박스 위치 설정
        printtext.text = text;

        yield return new WaitForSeconds(delay);

        Destroy(printtext);
    }


    //
    /*이벤트 함수 부분*/
    //

    public void OpneingText1()
    {
        StartCoroutine(FadeText("201x년 x월 x일",width * 0.3f,height * 0.4f,3,30,Color.red));
    }
}
