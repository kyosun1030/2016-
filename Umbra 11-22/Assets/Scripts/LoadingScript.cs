using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

    public GameObject text;

	// Use this for initialization
	void Start () {
        StartCoroutine(Loading());
	}

    IEnumerator Loading()
    {
        text.GetComponent<TextMesh>().text = "로딩중";

        yield return new WaitForSeconds(0.2f);

        text.GetComponent<TextMesh>().text = "로딩중.";

        yield return new WaitForSeconds(0.2f);

        text.GetComponent<TextMesh>().text = "로딩중..";

        yield return new WaitForSeconds(0.2f);

        text.GetComponent<TextMesh>().text = "로딩중..";

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Loading());
    }
}
