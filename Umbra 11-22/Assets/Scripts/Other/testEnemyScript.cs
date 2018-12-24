using UnityEngine;
using System.Collections;

public class testEnemyScript : MonoBehaviour {

    public GameObject player;
    public tesstScript playerScript;

	// Use this for initialization
	void Start () {

        playerScript = GameObject.Find("GameControl").GetComponent<tesstScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Vector3.Distance(this.gameObject.transform.position,player.transform.position) < 1)
        {
            playerScript.Stop = true;
        }
        else
        {
            playerScript.Stop = false;
        }
	}
}
