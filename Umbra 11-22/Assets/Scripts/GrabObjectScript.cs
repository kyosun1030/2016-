using UnityEngine;
using System.Collections;
using VRTK;

public class GrabObjectScript : MonoBehaviour {

  //  public GameObject controller;
    public VRTK_InteractableObject grabScript;
    public Color firstColor;
    public bool touchOn;
    public bool test;

    void Start()
    {
        grabScript = this.gameObject.GetComponent<VRTK_InteractableObject>();
        //controller = GameObject.FindWithTag("Controller (right)");
        //firstColor = this.gameObject.GetComponent<Renderer>().material.color;
        //StartCoroutine(Shine());
    }

	// Update is called once per frame
	void Update () {

        if(this.transform.childCount >0)
        {
            touchOn = false;
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
           // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
          //  this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        //print(grabScript.GetGrabbingObject());
        //if (grabScript.GetGrabbingObject() == this.gameObject)
        //{
        //    touchOn = false;
        //    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //}
        //else
        //{
        //    this.gameObject.GetComponent<BoxCollider>().enabled = true;
        //}

        //if (test)
        //{
        //    //StartCoroutine(Shine());
        //}
        //else
        //{
        //    //this.gameObject.GetComponent<Renderer>().material.SetColor("_SpecColor", firstColor);
        //    this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(0,0,0));
        //}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            touchOn = true;
            StartCoroutine(Shine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Controller")
        {
            touchOn = false;
        }
    }

    IEnumerator Shine()
    {
        for (float i = 0f; i < 0.5f; i += 0.01f)
        {
            Color color = new Vector4(i, i, i);
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        for (float i = 0.5f; i > 0f; i -= 0.01f)
        {
            Color color = new Vector4(i, i, i);
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            yield return new WaitForEndOfFrame();
        }

        if (touchOn)
        {
            StartCoroutine(Shine());
        }
    }

}
