using UnityEngine;
using System.Collections;

public class BaseEventTriggerScript : MonoBehaviour {

    public bool active = false;

    GameObject ghost;

    Vector3 chase1Way;
    Vector3 chase2Way;

    // 플레이어 & 이벤트 스크립트
    GameObject Player;
    GameObject reset;
    BaseEventScript eventScript;
    BaseGhostScript ghostScript;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        eventScript = GameObject.FindWithTag("Event").GetComponent<BaseEventScript>();
        ghostScript = GameObject.Find("Ghost").GetComponent<BaseGhostScript>();
        ghost = GameObject.Find("Ghost");

        reset = GameObject.Find("Reset");

        if(this.transform.name == "Chase1")
        {
            chase1Way = GameObject.Find("Chase1Way").transform.position;
        }

        if(this.transform.name == "Chase2")
        {
            chase2Way = GameObject.Find("Chase2Way").transform.position;
        }

        ghostScript.enabled = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Player.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {

                case "Drop":
                    Player.transform.position = reset.transform.position;
                    break;
                case "ChangeArea0":
                    //active = true;
                    ghostScript.AreaPass();
                    break;
                case "ChangeArea1":
                	//active = true;
                    ghostScript.AreaPass();
                    break;
				case "Start":
					eventScript.StartEvent1();
				    active = true;
					break;
                 case "ChaseStart":
                    ghostScript.enabled = true;
                	active = true;
                    break;
                 case "SetArea1":
                	active = true;
                    break;
				 case "Ending":
					eventScript.EndingStart();
                	active = true;
                    break; 
                 case "StartEscape":
                    eventScript.EscapeStart();
                	active = true;
                    break;
                 case "Chase1":
                    //if (ghostScript.playerdistance > 10)
                    //{
                    //    ghostScript.SetPosition(chase1Way);
                    //    ghostScript.SetTarget(this.gameObject);
                    //}
                	active = true;
                    break;
                 case "Chase2":
                    //if (ghostScript.playerdistance > 10)
                    //{
                    //    ghostScript.SetPosition(chase2Way);
                    //    ghostScript.SetTarget(this.gameObject);
                    //}
                	active = true;
                    break;
            }
        }
    }
}
