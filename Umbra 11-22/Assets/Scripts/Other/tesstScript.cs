using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tesstScript : MonoBehaviour {

    public List<GameObject> waypoint = new List<GameObject>();
    public GameObject player;

    public float speed;
    public int wpNum;

    public bool Stop;

	// Use this for initialization
	void Start () {

        Stop = false;

        wpNum = 0;
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(!Stop)
        {
            Go();
        }
	}

    void Go()
    {

        Vector3 dir = waypoint[wpNum].transform.position - player.transform.position;

        player.transform.position += dir.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(waypoint[wpNum].transform.position, player.transform.position) < 0.1f)
        {
            wpNum++;
            if (wpNum == waypoint.Count)
            {
                wpNum = 0;
            }
        }
    }
}
