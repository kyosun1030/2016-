using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneNameScript : MonoBehaviour {

    //public GameObject Objective;

    //void Start()
    //{
    //    StartCoroutine(TextCopy("1.수상한 것\n을 찾아보자"));
    //}

    public void TextCopyStart(string text,float sec)
    {
        StartCoroutine(TextCopy(text,sec));
    }

    IEnumerator TextCopy(string text, float sec)
    {
        int lengthmax = 0;

        while (lengthmax <= text.Length)
        {
            yield return new WaitForSeconds(0.1f);                                                  // 한글자씩 생성되는 주기

            string copy = text.Substring(0, lengthmax);                                              // 한글자씩 출력하기 위해.

            this.transform.GetComponent<TextMesh>().text = copy;                                          // 문자 입력
            lengthmax++;                                                                             // 문자 위치 변경 
        }

        yield return new WaitForSeconds(sec);

        for (float i = 1f; i >= 0; i -= 0.005f)
        {
            Color color = new Vector4(1, 1, 1, i);
            this.transform.GetComponent<TextMesh>().color = color;
            yield return null;
        }
    }
}
