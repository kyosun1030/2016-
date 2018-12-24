using UnityEngine;
using System.Collections;

public class k1Event : MonoBehaviour {

    public k1EventScript EventScript;

    public bool active;
    public GameObject Player;
    public GameObject Controller;

    
    

	// Use this for initialization
	void Start () {
        //EventScript = GameObject.FindWithTag("Event").GetComponent<k1GhostScript>();
	}

    


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Player.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {
                case "GhostZone":
                    EventScript.StartGhost();
                    active = true;
                    break;
                //case "GhostZone2":
                //    EventScript.StartGhost2();
                //    active = true;
                //    break;
                case "GhostZone3":
                    EventScript.StartGhost3();
                    active = true;
                    //수정
                    this.enabled = false;
                    break;
                case "GhostZone4":
                    EventScript.StartGhost4();
                    active = true;
                    break;
                case "SoundZone":
                    EventScript.StartSound();
                    active = true;
                    break;
                case "SoundZone2":
                    EventScript.StartSound2();
                    active = true;
                    break;
                //case "BottleCol":
                //    EventScript.StartGhost2();
                //    active = true;
                //    break;
            }
        }

        if (other.transform.gameObject == Controller.transform.gameObject && !active)
        {
            switch (this.transform.name)
            {
                case "SetBottle":
                    EventScript.StartGhost2();
                    active = true;
                    break;
                case "SetBook":
                    EventScript.StartGhost5();
                    active = true;
                    break;
            }
        }



        

    }


	
}
