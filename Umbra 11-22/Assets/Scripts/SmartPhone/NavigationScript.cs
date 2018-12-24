using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

    public Texture[] map;       // 바꿀 텍스쳐 배열


    void Start()
    {
        ChangeMap(2);
    }

    // 맵 변환 함수
    public void ChangeMap(int num)      // 메시지 변환할 숫자
    {
        // 현재 오브젝트의 렌더러 마테리얼 텍스쳐를 변경한다.
        this.GetComponent<Renderer>().material.SetTexture("_MainTex", map[num - 1]);
    }
}
