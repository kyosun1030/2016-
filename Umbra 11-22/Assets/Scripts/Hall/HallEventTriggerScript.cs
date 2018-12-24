using UnityEngine;
using System.Collections;

public class HallEventTriggerScript : MonoBehaviour {

    // 램프 이벤트
    public GameObject Lamp;

    public bool active = false;

    // 플레이어 & 이벤트 스크립트
    private GameObject Player;
    private HallEventScript eventScript;
    public NavigationScript naviScript;

    public SceneManagerScript sceneloadScript;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        eventScript = GameObject.FindWithTag("Event").GetComponent<HallEventScript>();
        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        //naviScript = GameObject.Find("Map").GetComponent<Navigationcript>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Player.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {
                case "Event1":
                    eventScript.Event1();
                    active = true;
                    break;
                case "Event2":
                    eventScript.Event2();
                    active = true;
                    break;
                case "Event4":
                    eventScript.Event4();
                    active = true;
                    break;
                case "Event5":
                    eventScript.Event5();
                    active = true;
                    break;
                case "Event6":
                    eventScript.Event6();
                    active = true;
                    break;
                case "Lamp1":                 
                    Lamp.transform.GetComponent<LampScript>().Active();
                    eventScript.way1.SetActive(false);
                    eventScript.way2.SetActive(true);
                    active = true;
                    break;
                case "Lamp2":                 
                    Lamp.transform.GetComponent<LampScript>().Active();
                    active = true;
                    break;
                case "KitchenLoad":
                    //sceneloadScript.MultiuseLoad();
                    sceneloadScript.BasementLoad();
                    active = true;
                    break;
                case "Floor1":
                    naviScript.ChangeMap(1);
                    break;
                case "Floor2":
                    naviScript.ChangeMap(2);
                    break;
            }
        }
    }
}
