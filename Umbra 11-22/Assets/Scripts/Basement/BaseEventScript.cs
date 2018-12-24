using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRTK;
public class BaseEventScript : MonoBehaviour {

    //
    /* 이 스크립트 상에서 모든 이벤트를 제어 할 것 */
    //
    public BGMPlayerScript bgmScript;
    public VRTK_TouchpadWalking touchWalkScript;
    public GameObject player;

    // 시간 이벤트를 위해 게임이 시작한 후 시간을 잰다.
    public float gameTime;

    // 일시정지 구상 중
    public bool isPause = false;

    //
    /* 씬처음 */
    //
    SceneNameScript sceneScript;
    SceneNameScript sceneScript2;
    //public GameObject phone;

    // 블랙 스크린
    GameObject black;

	//
	/*처음 이벤트 1*/
	//
	public GameObject key;
	public GameObject closeDoor;
	public GameObject[] lights;
	public GameObject spotlight;
	public GameObject spotlight2;

    public int hp;


    public SceneManagerScript sceneloadScript;

    public GameObject ending1;
    public GameObject ending2;

    public GameObject gameover;

    public PhoneMessageScript pmessageScript;

    public GameObject escapeposition;
    public BaseGhostScript ghostScript;

    // 원하는 이벤트 만큼 추가 시킬것 

	public int deviceOn = 0;


    void Start()
    {
		key.SetActive (false);
		//StartEvent1 ();
        //sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<BGMPlayerScript>();
        sceneScript = GameObject.Find("SceneName").GetComponent<SceneNameScript>();
        sceneScript2 = GameObject.Find("SceneName2").GetComponent<SceneNameScript>();
        black = GameObject.Find("Black");
        gameTime = 0;



		deviceOn = 0;

        touchWalkScript.maxWalkSpeed = 5;

        hp = 3;

        StartCoroutine(First());

       
    }

	public void StartEvent1()
	{
		StartCoroutine (Event1 ());
	}

    IEnumerator First()
    {
        yield return new WaitForSeconds(1);

        sceneScript.TextCopyStart("제 2 장", 3);

        yield return new WaitForSeconds(2);

        sceneScript2.TextCopyStart("Umbra", 2);

        yield return new WaitForSeconds(3);

        StartCoroutine(FirstFade(black));

        //phone.SetActive(true);

        //bgmScript.PlayBGM(0, true);
    }

	IEnumerator Event1()
	{
		while (closeDoor.transform.rotation.y < 0.7f)
		{
			closeDoor.transform.eulerAngles -= new Vector3(0, 5, 0);
			yield return new WaitForEndOfFrame();
		}

		closeDoor.GetComponent<AudioSource> ().Play ();

		yield return new WaitForSeconds (1);

		for (int i = 0; i < lights.Length; i++) {
			lights[i].GetComponent<AudioSource>().Play();
			lights[i].GetComponent<Light>().enabled = false;
			yield return new WaitForSeconds(0.2f);
		}

		bgmScript.PlayBGM (0, true);

		yield return new WaitForSeconds(2);

		key.SetActive (true);
		spotlight.SetActive (true);
		spotlight2.SetActive (true);

        pmessageScript.ChangeMessage(1);

        yield return new WaitForSeconds(3);

        pmessageScript.ChangeMessage(2);
	}

    public void EscapeStart()
    {
        StartCoroutine(Escape());
    }

    IEnumerator Escape()
    {
        ghostScript.SetPosition(escapeposition.transform.position);

        ghostScript.find_distance = 100f;
        ghostScript.chase_distance = 100f;

        yield return null;

    }

    public void DeadStart()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        StartCoroutine(EndingFade(black));

        yield return new WaitForSeconds(2);

        GameOverStart();

        bgmScript.PlayEffect(4, true);

    }

    public void GameOverStart()
    {
        StartCoroutine(EndingFade(gameover));
    }

    public void EndingStart()
    {
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        StartCoroutine(EndingFade(black));

		touchWalkScript.maxWalkSpeed = 0;

        bgmScript.PlayBGM(2, true);
        bgmScript.PlayEffect(3, true);

        StartCoroutine(EndingFade(ending1));

        yield return new WaitForSeconds(8);

        StartCoroutine(EndingFade(ending2));


        yield return new WaitForSeconds(10);

        ending1.SetActive(false);
        ending2.SetActive(false);
 
        GameOverStart();
        yield return null;
    }

    IEnumerator EndingFade(GameObject obj)
    {
        for (float i = 0; i <= 1f; i += 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }
    }

    public IEnumerator FadeInOut(GameObject obj, float sec)
    {
        // In이면 true, Out이면 fasle를 주면된다.
        // 노파심에 FadeIn이 투명->원본 점점 나타나는것
        // FadeOut이 원본->투명으로 점점 사라지는것.

        for (float i = 0; i <= 1f; i += 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }

        yield return new WaitForSeconds(sec);

        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }
    }

    IEnumerator FirstFade(GameObject obj)
    {
        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            // obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return 0;
        }
    }
}
