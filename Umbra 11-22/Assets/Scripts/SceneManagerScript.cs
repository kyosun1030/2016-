using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerScript : MonoBehaviour {

    AsyncOperation async;

	void Start () {
        DontDestroyOnLoad(transform.gameObject);
	}
	
    public void OpeningLoad()
    {
       // SceneManager.LoadScene("Opening");
    }

    public void HallLoad()
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(Load("Hall"));
    }

    public void MultiuseLoad()
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(Load("Multiuse"));
    }

    public void RoomLoad()
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(Load("Room"));
    }

    public void BasementLoad()
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(Load("Basement"));
    }

    IEnumerator Load(string name)
    {
        yield return new WaitForSeconds(1);

        async = SceneManager.LoadSceneAsync(name);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(3);

        SceneManager.UnloadScene("Load");

        async.allowSceneActivation = true;
    }

    //IEnumerator Loading(string name)
    //{
    //    if (async.isDone)
    //    {
    //        yield return new WaitForSeconds(2);

    //        SceneManager.UnloadScene("Load");

    //        async.allowSceneActivation = true;
    //    }
    //    else
    //    {
    //        StartCoroutine(Loading(name));
    //    }

    //    yield return null;
    //}
}
