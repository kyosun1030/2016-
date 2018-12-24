using UnityEngine;
using System.Collections;
using System.IO;

public class CaptureScript : MonoBehaviour {

    public Camera cam;

    public GameObject saveLocation;
    public GameObject ps;

    public GameObject[] gallery = new GameObject[6];

    public int count;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(capture());
        }
	}

    IEnumerator capture()
    {

        RenderTexture rt = new RenderTexture(256, 256, 24);
        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;

        Texture2D sc = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
        sc.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
        sc.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;


        Sprite make = Sprite.Create(sc, new Rect(0, 0, sc.width, sc.height), new Vector2(0, 0));

        GameObject temp = new GameObject();

        temp.transform.SetParent(saveLocation.transform);
        temp.AddComponent<SpriteRenderer>();
        temp.GetComponent<SpriteRenderer>().sprite = make;
        temp.transform.position = ps.transform.position;
        temp.transform.localScale = ps.transform.localScale;
        temp.transform.localEulerAngles = new Vector3(0, 180, 0);
        temp.transform.name = "saveCapture" + count;

        

        count++;

        yield return new WaitForSeconds(1.5f);

        temp.SetActive(false);
    }
}
