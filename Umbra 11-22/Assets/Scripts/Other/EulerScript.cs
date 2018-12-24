using UnityEngine;
using System.Collections;

public class EulerScript : MonoBehaviour {

    public GameObject LeftShoulder;


    // 2초동안 50만큼의 xyz각도를움직이고 싶다.
    // StartCoroutine(Euler(LeftShoulder, 2, 50, x));

    void Start()
    {
        StartCoroutine(Euler(LeftShoulder, 3, -140, 'y'));
    }

    public IEnumerator Euler(GameObject obj,int sec ,int angle, char xyz)
    {
        int count = 0;
        float num = sec * 20;

        float valueX = 0, valueY = 0, valueZ = 0;

        if (xyz == 'x')
        {
            valueX = angle / sec * 0.1f;
        }
        else if (xyz == 'y')
        {
            valueY = angle / sec * 0.1f;
        }
        else if (xyz == 'z')
        {
            valueZ = angle / sec * 0.1f;
        }

        while (count < num)
        {
            yield return new WaitForSeconds(0.01f);
            obj.transform.eulerAngles += new Vector3(valueX, valueY, valueZ);
            count++;
        }
    }
}
