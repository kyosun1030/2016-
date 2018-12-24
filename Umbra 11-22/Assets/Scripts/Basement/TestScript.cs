using UnityEngine;
using System.Collections;


public class TestScript : MonoBehaviour {

    public Vector3 position;
    public float distance;
    public Vector3 direction;
    public Vector3 dir;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        position = this.transform.position - player.transform.position;
        distance = position.magnitude;
        direction = position.normalized;
	}
}
