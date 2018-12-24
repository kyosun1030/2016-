using UnityEngine;
using System.Collections;

public class OpEventTriggerScript : MonoBehaviour {

    public bool active = false;
    
    // 플레이어 & 이벤트 스크립트
    private GameObject Player;
    private OpEventScript eventScript;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        eventScript = GameObject.FindWithTag("Event").GetComponent<OpEventScript>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Player.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {
                case "Event2":
                    eventScript.Event2Start();
                    active = true;
                    break;
            }
        }
    }
}
