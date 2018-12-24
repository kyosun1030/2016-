using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    GameObject device;
    GameObject setKey;
    GameObject startdoor;
    GameObject finaldoor;

	GameObject on;

	BaseEventScript eventScript;

    public int activeNum = 0;

	void Start () {
        device = this.transform.parent.gameObject;
        setKey = this.transform.Find("SetKey").gameObject;
		on = this.transform.Find ("On").gameObject;

		startdoor = GameObject.Find ("StartDoor");
		finaldoor = GameObject.Find ("FinalDoor");

		eventScript = GameObject.FindWithTag ("Event").GetComponent<BaseEventScript> ();

        setKey.SetActive(false);
		on.SetActive (false);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Key")
        {
			//her.GetComponent<BoxCollider>().isTrigger = false;
			//other.transform.SetParent(this.transform);
			//other.transform.position = setKey.transform.position;
			on.SetActive(true);
			other.gameObject.SetActive(false);
            StartCoroutine(MoveDevice());
        }
    }

    IEnumerator MoveDevice()
    {
        while(device.transform.position.y > -2.5f)
        {
            device.transform.position += new Vector3(0, -0.01f, 0);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
	
        if (eventScript.deviceOn == 0)
        {
			
			startdoor.GetComponent<AudioSource>().Play();

            while (startdoor.transform.position.y > -3)
            {
                startdoor.transform.position += new Vector3(0, -0.01f, 0);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);

            startdoor.GetComponent<AudioSource>().Stop();
        }

		eventScript.deviceOn++;

		if(eventScript.deviceOn > 3)
        {
			print("되냐?");
            while (finaldoor.transform.position.y > -3)
            {
                finaldoor.transform.position += new Vector3(0, -0.05f, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
