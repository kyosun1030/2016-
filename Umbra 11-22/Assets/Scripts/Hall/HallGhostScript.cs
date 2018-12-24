using UnityEngine;
using System.Collections;

public class HallGhostScript : MonoBehaviour {

    public GameObject Player;

    public Animator ghostAni;

    //public Camera phoneCamera;
    //public GameObject Display;

    public AudioSource audio;
    public AudioClip[] audioClip;

    public bool inCamera;

    public float speed;
    //public float hideTime;
    public float distance;
    public float waydistance;
    //public int[] randomLocation = new int [2] {-10,10};


    //public bool Idle;
    //public bool walking;
    //public bool walkingStair;
    public bool find;
    public bool chase;

    public GameObject[] wayPoint;
    public Vector3 destination;
    public GameObject setPosition;
    private Vector3 dir;

    public DoorScript door;
    public GameObject openposition;
    public bool active;

    public GameObject girllight;

    GameObject[] bodys;

    void Start()
    {
        //girllight = this.GetComponentInChildren<Light>();
        destination = wayPoint[Random.Range(0,wayPoint.Length)].transform.position;
        ghostAni.SetBool("Walking", true);
        audio.clip = audioClip[0];
        audio.Play();
    }

    void OnEnable()
    {
        bodys = GameObject.FindGameObjectsWithTag("Child");
    }

	void Update () {

        if (!door.open)
        {
            if (!find)
            {
                distance = Vector3.Distance(Player.transform.position, this.transform.position);
                //audio.volume = 1 - distance * 0.1f;
                //if (distance < 5)
                //{
                //    Chase();
                //}
                //else
                //{
                    Move();
               // }
            }
        }
        else if (door.open && !active)
        {
            StartCoroutine(Door());
            active = true;
        }
	}

    void Chase()
    {
        if (distance > 0.5f)
        {
            dir = Player.transform.position - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.deltaTime;
            transform.LookAt(Player.transform.position);
        }
        else
        {
            audio.Stop();
            audio.volume = 1;
            //transform.LookAt(Player.transform.position);
            ghostAni.SetBool("Walking", false);
            StartCoroutine(Find());      
            find = true;
        }
    }

    void Move()
    {
        waydistance = Vector3.Distance(destination , this.transform.position);

        transform.LookAt(destination);

        if (waydistance > 0.5f)
        {
            dir = destination - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            destination = wayPoint[Random.Range(0, wayPoint.Length)].transform.position;
        }
    }

    IEnumerator Find()
    {
        this.transform.position = setPosition.transform.position;
        transform.LookAt(Player.transform.position);

        yield return new WaitForSeconds(3);

        gameObject.layer = 0;
        audio.clip = audioClip[1];
        audio.loop = false;
        audio.Play();

        yield return new WaitForSeconds(4f);

        this.gameObject.SetActive(false);
    }

    IEnumerator Door()
    {
        ghostAni.SetBool("Walking", false);
        this.transform.position = openposition.transform.position;
        transform.LookAt(Player.transform.position);

        yield return new WaitForSeconds(1);

        girllight.SetActive(true);
		for (int i = 0; i < bodys.Length; i++) 
		{
			bodys[i].layer = 0;
		}
        audio.clip = audioClip[1];
        audio.loop = false;
        audio.Play();

         yield return new WaitForSeconds(1);

        this.gameObject.SetActive(false);
    }
}
