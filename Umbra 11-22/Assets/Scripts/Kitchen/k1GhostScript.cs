using UnityEngine;
using System.Collections;

public class k1GhostScript : MonoBehaviour {

    public GameObject Player;
    public GameObject Ghost1;



    // Use this for initialization
    void Start()
    {

    }

    public void StartGhost()
    {
        StartCoroutine(GhostCome());
    }

    IEnumerator GhostCome()
    {
        Ghost1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Ghost1.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Ghost1.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Ghost1.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Ghost1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Ghost1.SetActive(false);
        yield return new WaitForSeconds(0.2f);
    }
}
