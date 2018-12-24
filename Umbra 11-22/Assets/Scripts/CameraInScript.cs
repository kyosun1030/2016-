using UnityEngine;
using System.Collections;

public class CameraInScript : MonoBehaviour {

    public Camera phoneCamera;
    public float distance;

    public bool Complete = false;


	// Update is called once per frame
	void Update () {

        if (phoneCamera.isActiveAndEnabled
            && Vector3.Distance(phoneCamera.transform.position,this.transform.position) < distance
            && !Complete)
        {
            Check();
        }
	}

    void Check()
    {
        Vector3 targetScreenPos = phoneCamera.WorldToViewportPoint(this.transform.position);
        if (targetScreenPos.x < 1f && targetScreenPos.x > 0
            && targetScreenPos.y < 1f && targetScreenPos.y > 0
            && targetScreenPos.z < 5 && targetScreenPos.z > 0)
        {
            //this.gameObject.SetActive(true);
            //if (!OnePlay)
            //{
            //    StartCoroutine(ObjFadeIn(this.gameObject));
            //}
            if (this.gameObject.GetComponent<Renderer>().material.color.a != 1)
            {
                this.gameObject.GetComponent<Renderer>().material.color =
                    new Vector4(1, 1, 1, gameObject.GetComponent<Renderer>().material.color.a + 0.04f);
            }

            if (this.transform.GetComponent<Renderer>().material.color.a > 0.9f)
            {
                Complete = true;
            }
        }
        else
        {
            //OnePlay = false;
            //this.gameObject.SetActive(false);
            if (this.gameObject.GetComponent<Renderer>().material.color.a != 1)
            {
                this.gameObject.GetComponent<Renderer>().material.color =
                    new Vector4(1, 1, 1, gameObject.GetComponent<Renderer>().material.color.a - 0.02f);
            }
        }
    }

    //public IEnumerator ObjFadeIn(GameObject obj)
    //{
    //    // In이면 true, Out이면 fasle를 주면된다.
    //    // 노파심에 FadeIn이 투명->원본 점점 나타나는것
    //    // FadeOut이 원본->투명으로 점점 사라지는것.

    //    for (float i = 0; i <= 1f; i += 0.02f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<Renderer>().material.color = color;
    //        yield return 0;
    //    }
    //    Complete = true;
    //}

    //public IEnumerator ObjFadeInOut(GameObject obj, float sec)
    //{
    //    // In이면 true, Out이면 fasle를 주면된다.
    //    // 노파심에 FadeIn이 투명->원본 점점 나타나는것
    //    // FadeOut이 원본->투명으로 점점 사라지는것.


    //    for (float i = 0; i <= 1f; i += 0.02f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<Renderer>().material.color = color;
    //        yield return 0;
    //    }

    //    yield return new WaitForSeconds(sec);

    //    for (float i = 1f; i >= 0; i -= 0.02f)
    //    {
    //        Color color = new Vector4(1, 1, 1, i);
    //        obj.GetComponent<Renderer>().material.color = color;
    //        yield return 0;
    //    }
    //}
}
