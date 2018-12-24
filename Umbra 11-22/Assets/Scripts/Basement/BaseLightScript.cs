using UnityEngine;
using System.Collections;

public class BaseLightScript : MonoBehaviour {

    public GameObject player;
    public GameObject ghost;
    public GameObject woodstick;

    public Light light;

    public bool active;
    public bool playerlight;

    public Color playerColor;
    public Color ghostColor;

    public AudioSource audio;

    public bool off;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        ghost = GameObject.Find("Ghost");
        light = this.GetComponentInParent<Light>();
        playerColor = this.GetComponentInParent<Light>().color;
        ghostColor = new Vector4(1, 0, 0, 1);

        audio = this.GetComponentInParent<AudioSource>();
	}

    void FixedUpdate()
    {
        if(light.enabled && off)
        {
            StartCoroutine(LightOff());
            off = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            //case "Player":
            //    if(!active)
            //    {
            //        playerlight = true;
            //        light.color = playerColor;
            //        StartCoroutine(LightOn());
            //    }
            //    break;
            case "Ghost":
                if (!active)
                {
                    light.color = ghostColor;
                    StartCoroutine(LightOn());
                }
                break;
            case "Stone":
                if (!active)
                {
                    playerlight = true;
                    light.color = playerColor;
                    StartCoroutine(LightOn());
                }
                break;
        }
    }


    IEnumerator LightOn()
    {
        active = true;
        light.enabled = true;

        yield return new WaitForSeconds(4);

        active = false;
        light.enabled = false;
        playerlight = false;
        audio.Play();
    }

    IEnumerator LightOff()
    {
        yield return new WaitForSeconds(3);

        active = false;
        light.enabled = false;
        playerlight = false;
        audio.Play();
    }
}
