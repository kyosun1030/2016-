using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class BaseGhostScript : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent nma;
    Animator ani;
    GameObject player;

	Transform setRotaition;

    public List<GameObject> firstway = new List<GameObject>();
    public List<GameObject> secondway = new List<GameObject>();

    public GameObject movingTarget;
    public float distance;
    public float playerdistance;
    public Vector3 playerdir;
    public int areaSelect = 0;

    public bool chase = false;
    public bool hold;

    public float find_distance;
    public float chase_distance;

	public float gameTime;
	float oldTime;

	public AudioSource audio;
    AudioSource voiceAudio;
    AudioSource playerAudio;
    
    public AudioClip[] whereList;
    public AudioClip[] findList;
    public AudioClip[] missList;
    public AudioClip[] catchList;
    public AudioClip[] endingList;
    public AudioClip[] chaseList;

    BGMPlayerScript bgmScript;
    BaseEventScript eventScript;
    public VRTK_TouchpadWalking touchWalkScript;

    float chaseout_interval = 2f;
    float chaseout_elpased;

    float chase_interval = 2f;
    float chase_elpased;

    float whereVoice_interval = 10f;
    float whereVoice_elpased;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        bgmScript = GameObject.FindWithTag("GameController").GetComponent<BGMPlayerScript>();
        eventScript = GameObject.FindWithTag("Event").GetComponent<BaseEventScript>();
        ani = this.gameObject.GetComponent<Animator>();
        voiceAudio = this.gameObject.GetComponent<AudioSource>();

        nma = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

		setRotaition = this.transform;

        ani.SetBool("Walking", true);
	}
	

	// Update is called once per frame
	void FixedUpdate () {

        playerdir = (this.transform.position - player.transform.position).normalized;
        playerdistance = Vector3.Distance(player.transform.position, this.transform.position);
	
        if (!hold) {
			if (chase) {
                chase_elpased += Time.deltaTime;
                if (audio.clip.name != "chase" || !audio.isPlaying) {
					bgmScript.PlayBGM (1, false);
				}
				whereVoice_elpased = 0;
				//this.transform.LookAt (player.transform);
				ChasePlayer ();
			} else if (!chase) {
                chase_elpased = 0;
				if (movingTarget == null) {
					PatrolSet ();
				} else if (movingTarget != null) {
					distance = Vector3.Distance (movingTarget.transform.position, this.transform.position);
					ChaseCheck ();
					Patrol ();
                    touchWalkScript.maxWalkSpeed = 5;
				}
			}
		} 
		else 
		{
			nma.speed = 0;
		}

        if(playerdistance < 1f)
        {
            StartCoroutine(meet());
        }
	}

    void PatrolSet()
    {
        if (areaSelect == 0)
        {
            movingTarget = firstway[Random.Range(0, 9)];

        }
        else if (areaSelect == 1)
        {
            movingTarget = secondway[Random.Range(0, 9)];
        }
    }

    void ChaseCheck()
    {
        if (playerdir.x > -0.5f)
        {
            if(playerdistance < find_distance)
            {
                chase = true;  
                StartCoroutine(ActiveAni());
            }
        }

        if (playerdir.x < -0.5f)
        {
            if(playerdistance < find_distance - 10)
            {
                chase = true;
                StartCoroutine(ActiveAni());
            }
        }
    }

    void Patrol()
    {
        nma.speed = 3;
        nma.SetDestination(movingTarget.transform.position);
        bgmScript.Stop(false);

        whereVoice_elpased += Time.deltaTime;

        if (whereVoice_elpased > whereVoice_interval)
        {
            voiceAudio.clip = whereList[Random.Range(0, whereList.Length)];
			voiceAudio.pitch = 1;
			voiceAudio.Play();
			whereVoice_elpased = 0;
        }

        if (distance < 1f)
        {
            movingTarget = null;
        }
    }

    void ChasePlayer()
    {
		if (playerdistance < chase_distance - 5) {
			touchWalkScript.maxWalkSpeed = 9;
		}
        nma.SetDestination(player.transform.position);

        if (playerdistance < chase_distance)
        {
            chaseout_elpased = 0;
            if(chase_elpased > chase_interval)
            {
                voiceAudio.clip = chaseList[Random.Range(0, chaseList.Length)];
				voiceAudio.Play();
				if(chase_elpased > 10)
				{
               		 chase_elpased = 0;
				}
            }
        }
        else
        {
            chaseout_elpased += Time.deltaTime;

            if (chaseout_elpased > 2)
            {
                chase = false;
                bgmScript.Stop(false);

                ani.SetBool("Pointing", false);
                ani.SetBool("Running", false);
                ani.SetBool("Walking", true);

                touchWalkScript.maxWalkSpeed = 5;

                movingTarget = null;

                voiceAudio.clip = missList[Random.Range(0, missList.Length)];
				voiceAudio.pitch = 1;
				voiceAudio.Play();

                chase_elpased = 0;
                chaseout_elpased = 0;

                nma.speed = 3;
            }
        }
    }

    IEnumerator meet()
    {
        this.transform.position = new Vector3(0, 0, 0);

        eventScript.hp--;

        hold = true;
        chase = false;
		this.transform.rotation = setRotaition.rotation;
        nma.speed = 0;

        ani.SetBool("Pointing", false);
        ani.SetBool("Running", false);
        ani.SetBool("Walking", true);
        
        if (eventScript.hp == 2)
        {
            voiceAudio.clip = catchList[0];
			voiceAudio.pitch = 1;
            voiceAudio.Play();
        }
        else if(eventScript.hp == 1)
        {
            voiceAudio.clip = catchList[1];
			voiceAudio.pitch = 1;
			voiceAudio.Play();
        }
        else if(eventScript.hp == 0)
        {
            voiceAudio.clip = catchList[2];
			voiceAudio.pitch = 1;
            eventScript.DeadStart();
			voiceAudio.Play();
        }

        yield return new WaitForSeconds(10);

        hold = false;
        chase = false;

		if (areaSelect == 1)
		{
			this.transform.position = firstway[Random.Range(0, 9)].transform.position;
		}
		else if (areaSelect == 0)
		{
			this.transform.position = secondway[Random.Range(0, 9)].transform.position;
		}

		nma.speed = 3;
    }

    IEnumerator ActiveAni()
    {
        hold = true;
        nma.speed = 0;

        this.transform.LookAt(player.transform);

        ani.SetBool("Walking", false);
        ani.SetBool("Pointing", true);

        voiceAudio.clip = findList[Random.Range(0, findList.Length)];

        yield return new WaitForSeconds(2);

        ani.SetBool("Pointing", false);
        ani.SetBool("Running", true);
        hold = false;
        nma.speed = 5.5f;
    }

    public void SetPosition(Vector3 set)
    {
        this.transform.position = set;
    }

    public void SetTarget(GameObject set)
    {
        movingTarget = set;
    }

    public void AreaPass()
    {
        if (areaSelect == 0)
        {
            areaSelect = 1;
            movingTarget = secondway[Random.Range(0, 9)];
        }
        else if (areaSelect == 1)
        {
            areaSelect = 0;
            movingTarget = firstway[Random.Range(0, 9)];
        }
    }
}
