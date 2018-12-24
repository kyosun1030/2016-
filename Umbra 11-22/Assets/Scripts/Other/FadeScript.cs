using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScript : MonoBehaviour {

    //
    /* FadeInOut */
    //
    public IEnumerator FadeInOut(bool InOut)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.
        if (InOut)
        {
            for (float i = 0; i <= 1f; i += 0.05f)
            {
                Color color = new Vector4(1, 1, 1, i);
                transform.GetComponent<Renderer>().material.color = color;
                yield return 0;
            }
        }
        else
        {
            for (float i = 1f; i >= 0; i -= 0.05f)
            {
                Color color = new Vector4(1, 1, 1, i);
                transform.GetComponent<Renderer>().material.color = color;
                yield return 0;
            }
        }
    }
}
