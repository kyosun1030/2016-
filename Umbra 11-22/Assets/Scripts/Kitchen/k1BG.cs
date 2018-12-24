using UnityEngine;
using System.Collections;

public class k1BG : MonoBehaviour {

    public GameObject Lightning;

    public AudioSource AS;
    public AudioClip AC;

    
    void Start()
    {
        StartLightning();
    }
    

	public void StartLightning()
    {
        StartCoroutine(LightningOn());
    }

    IEnumerator LightningOn()
    {
        Lightning.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Lightning.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        Lightning.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Lightning.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Lightning.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Lightning.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        Lightning.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Lightning.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Lightning.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Lightning.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.2f);
    }
}
