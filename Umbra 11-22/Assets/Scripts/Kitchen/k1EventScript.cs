using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class k1EventScript : MonoBehaviour {

    public BGMPlayerScript bgmScript;

    
    public bool active;

    public GameObject ghost;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;

    public GameObject ghostpo;

    public GameObject Player;

    public GameObject Statue;

    AudioSource audio;
    public AudioClip SndPlay;
    public AudioClip SndPlay2;
    public AudioClip SndPlay3;
    public AudioClip EndPlay1;
    public AudioClip EndPlay2;
    public AudioClip SndPlay4;
    public AudioClip SndPlay5;
    

    public bool alreadyPlayed = false;
    public float Volume = 0.5f;
    public float Volume2 = 0.4f;

    public k1BG BGScript;
    public k1BG BGScript1;

    public k1SkeletonScript SkScript;
    
    
    public float ghostSpeed = 5.0f;
    public float ghostSpeed2 = 6.0f;
    //bool isCome = false;

    public int setObjNum = 0;

    public SceneNameScript sceneScript;
    public SceneNameScript sceneScript2;
    public GameObject black;
    public GameObject white;
    public GameObject player;
    public GameObject light;

    public SceneManagerScript sceneloadScript;

    public GameObject phone;

    public PhoneMessageScript pmessageScript;

	void Awake() {
        audio = GetComponent<AudioSource>();

        sceneScript = GameObject.Find("SceneName").GetComponent<SceneNameScript>();
        sceneScript2 = GameObject.Find("SceneName2").GetComponent<SceneNameScript>();

        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

       StartCoroutine(First());

       phone.SetActive(false);
       //EndStart();

      
    }



    IEnumerator First()
    {
        yield return new WaitForSeconds(1);

        sceneScript.TextCopyStart("제 2 장", 3);

        yield return new WaitForSeconds(2);

        sceneScript2.TextCopyStart("불안감", 1);

        yield return new WaitForSeconds(5);

        StartCoroutine(FirstFade(black));

        phone.SetActive(true);

        pmessageScript.ChangeMessage(1);

        yield return new WaitForSeconds(2);

        pmessageScript.ChangeMessage(2);
    }

    public void StartGhost()
    {
        //StartCoroutine(GhostCome());
        
        
        if (!alreadyPlayed)
        {
            audio.PlayOneShot(EndPlay2, Volume);
            //수정 alreadyPlayed = true;
        }
    }

    public void StartGhost2()
    {
        StartCoroutine(GhostCome2());
        if (!alreadyPlayed)
        {
            audio.PlayOneShot(EndPlay2, Volume);
            //수정 alreadyPlayed = true;
        }
    }

    public  void StartGhost3()
    {
        StartCoroutine("GhostCome3", 2.0f);
        
        if (!alreadyPlayed)
        {
            audio.PlayOneShot(EndPlay2, Volume);
            //수정 alreadyPlayed = true;
        }
    }

    public void StartGhost4()
    {
        //ghost4.transform.Translate(Vector3.forward * Time.deltaTime * ghostSpeed);
        StartCoroutine("Ghost4Start", 5.0f);
    }

    public void StartGhost5()
    {
        StartCoroutine("Ghost5Start", 3.0f);
    }
    

    public void StartSound()
    {
        if(!alreadyPlayed)
        {
            
            audio.PlayOneShot(SndPlay2, Volume);
                //수정 alreadyPlayed = true;
        }
    }

    public void StartSound2()
    {
        BGScript.StartLightning();
        BGScript1.StartLightning();
        if(!alreadyPlayed)
        {
            audio.PlayOneShot(SndPlay3, Volume2);
            //수정 alreadyPlayed = true;
        }
    }

    public void EndSoundStart()
    {
            audio.PlayOneShot(EndPlay1, Volume2);
    }

    public void EndSoundStart2()
    {
            audio.PlayOneShot(EndPlay2, Volume2);
    }

    IEnumerator GhostCome()
    {
        audio.PlayOneShot(SndPlay5, Volume2);
        
        ghost.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ghost.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator GhostCome2()
    {
        ghost2.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        ghost2.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        
    }

    IEnumerator GhostCome3(float _time)
    {
        float startTime = Time.timeSinceLevelLoad;
        audio.PlayOneShot(SndPlay5, Volume2);
        ghost3.SetActive(true);
        
        while (Time.timeSinceLevelLoad <= startTime + _time) {
            ghost3.transform.Translate(Vector3.up * Time.deltaTime * ghostSpeed2);
            yield return new WaitForEndOfFrame();
        }

        ghost3.SetActive(false);
    }

    IEnumerator Ghost4Start(float _time)
    {
        float startTime = Time.timeSinceLevelLoad;

        ghost4.SetActive(true);
        

        while (Time.timeSinceLevelLoad <= startTime + _time)
        {
            ghost4.transform.Translate(Vector3.forward * Time.deltaTime * ghostSpeed);
            yield return new WaitForEndOfFrame();
        }

        ghost4.SetActive(false);

    }

    IEnumerator Ghost5Start()
    {
        
        audio.PlayOneShot(SndPlay4, Volume2);

        for (int i = 0; i < 18; i++)
        {

            Statue.transform.position = Statue.transform.position + new Vector3(0, 0, 0.2f);

            yield return new WaitForSeconds(0.19f);
            
            Statue.transform.position = Statue.transform.position + new Vector3(0, 0, -0.2f);
            yield return new WaitForSeconds(0.19f);
        }

        
    }


	void Update () {
        
        alreadyPlayed = false;

        
    }

    IEnumerator FirstFade(GameObject obj)
    {
        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            //obj.GetComponent<CanvasRenderer>().SetColor(color);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FirstOut(GameObject obj)
    {
        for (float i = 0; i <= 1f; i += 0.02f)
        {
            Color color = new Vector4(1, 1, 1, i);
            obj.GetComponent<Renderer>().material.color = color;
            yield return new WaitForEndOfFrame();
        }
    }

    public void EndStart()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {

        yield return new WaitForSeconds(2);

        bgmScript.PlayEffect(0, false);

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 70;i++)
        {
            light.GetComponent<Light>().intensity += 0.1f;
            yield return new WaitForEndOfFrame();
        }
        bgmScript.PlayEffect(1, false);

        StartCoroutine(FirstOut(white));


        yield return new WaitForSeconds(3);

        sceneloadScript.RoomLoad();
    }
    
    //void Check()
    //{
    //    float distance = Vector3.Distance(tag.Player.transform.position, transform.position);

    //    if(distance <= 2)
    //    {
    //        isCome = true;
    //    }
    //}

    //void UnCheck()
    //{
    //    float distance = Vector3.Distance(Player.transform.position, transform.position);

    //    if(distance > 2)
    //    {
    //        isCome = false;
    //    }
    //}
}
