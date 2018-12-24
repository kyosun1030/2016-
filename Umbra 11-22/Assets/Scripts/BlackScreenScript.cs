using UnityEngine;
using System.Collections;

public class BlackScreenScript : MonoBehaviour {

    public Camera main;

	
	// Update is called once per frame
	void Update () {
        this.transform.rotation= main.transform.rotation;
	}
}
