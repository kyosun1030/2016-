using UnityEngine;
using System.Collections;

public class r1blowup : MonoBehaviour {
    public GameObject blowup;
    public GameObject trigger;
    
	// Use this for initialization
    
	// Update is called once per frame
	void OnTriggerEnter(){
	if(trigger.name=="Player")
        {
       blowup.GetComponent<Rigidbody>().AddForce(transform.up * 50f);
       blowup.GetComponent<Rigidbody>().useGravity = true;

        }
	}
	
}
