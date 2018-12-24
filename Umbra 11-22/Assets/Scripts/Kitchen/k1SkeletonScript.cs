using UnityEngine;
using System.Collections;

public class k1SkeletonScript : MonoBehaviour {

    public GameObject Player;
    public bool isSearch = false;
    public float speed = 3;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Search();
        transform.LookAt(Player.transform);
	}

    void Search()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if(distance <=5)
        {
            if (!isSearch)
            {
                isSearch = true;
                StartCoroutine(Running());
            }
        }
    }

    IEnumerator Running()
    {
        if(isSearch)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed);
        }

        yield return new WaitForSeconds(1);

        this.gameObject.SetActive(false);
    }
}

