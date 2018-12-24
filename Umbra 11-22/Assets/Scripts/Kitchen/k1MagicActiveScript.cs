using UnityEngine;
using System.Collections;

public class k1MagicActiveScript : MonoBehaviour {
    public GameObject activeMatObj;
    public GameObject setObj;
    public k1EventScript eventScript;
    public k1BG k1bgScript;

    public GameObject EndBook;
    public GameObject EndScroll;
    public GameObject EndBottle;

    public GameObject MagicEffect1;
    public GameObject MagicEffect2;
    public GameObject MagicEffect3;

    void Start()
    {
        eventScript = GameObject.FindWithTag("Event").GetComponent<k1EventScript>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == activeMatObj.name)
        {
            setObj.SetActive(true);
            activeMatObj.SetActive(false);
            eventScript.setObjNum++;

            if(eventScript.setObjNum == 1)
            {
                eventScript.EndSoundStart();
            }

            if(eventScript.setObjNum == 2)
            {
                eventScript.EndSoundStart2();
                k1bgScript.StartLightning();
            }

            if (eventScript.setObjNum == 3)
            {
                
                eventScript.EndStart();
            }
        }
    }

    void Update()
    {
        if(EndBook.activeSelf)
        {
            MagicEffect1.SetActive(true);
        }

        if(EndBottle.activeSelf)
        {
            MagicEffect2.SetActive(true);
            
        }

        if(EndScroll.activeSelf)
        {
            MagicEffect3.SetActive(true);
            
        }
    }
}
