using UnityEngine;

public class LampController : MonoBehaviour
{

    public bool lampOn = false; // PLC의 출력 비트라고 생각하세요.
    private Renderer rend;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 이 물체의 Renderer(그리기를 담당하는 컴포넌트)를 가져옵니다.
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lampOn)
        {
            // 켜졌을 때: 노란색
            rend.material.color = Color.yellow;
        }
        else
        {
            // 꺼졌을 때: 검은색
            rend.material.color = Color.black;
        }
    }
}
