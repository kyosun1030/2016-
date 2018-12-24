using UnityEngine;
using System.Collections;

public class PrEventTriggerScript : MonoBehaviour {

    public bool active = false;
    
    // 플레이어 & 이벤트 스크립트
    private GameObject Player;
    private PrEventScript eventScript;

    SceneManagerScript sceneloadScript;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        eventScript = GameObject.FindWithTag("Event").GetComponent<PrEventScript>();

        sceneloadScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Player.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {
                case "NextLoad":
                    eventScript.NextScene();
                    active = true;
                    break;
                case "Hall":
                    sceneloadScript.HallLoad();
                    break;
                case "MultiUse":
                    sceneloadScript.MultiuseLoad();
                    break;
                case "Room":
                    sceneloadScript.RoomLoad();
                    break;
                case "Basement":
                    sceneloadScript.BasementLoad();
                    break;
            }
        }
    }
}
