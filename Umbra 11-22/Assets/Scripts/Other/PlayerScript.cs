using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Camera mainCamera;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Door")
                {
                    print("되냐");
                    DoorScript doorScript = hit.transform.GetComponentInParent<DoorScript>();
                    //doorScript.StartCoroutine(doorScript.DoorOpen(hit.transform.parent.gameObject, 9, 120));
                }
            }
        }
	}
}
