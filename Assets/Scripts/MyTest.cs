using UnityEngine;
using TMPro;

public class MyTest : MonoBehaviour
{

    public string firstName ;

    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Hello !{firstName}!";
    }
    void Update()
    {
        // 큐브가 회전하게 만드는 코드
        //transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);

        // Y축 방향(위쪽)으로 매 프레임 일정 속도만큼 이동합니다.
        //float upSpeed = 1.0f; // 숫자가 커질수록 빨리 올라갑니다.
        //transform.Translate(Vector3.up * upSpeed * Time.deltaTime);

        // 0에서 5 사이를 왕복하는 값 계산
        float move = Mathf.PingPong(Time.time * 2.0f, 5.0f);

        // 시작 위치(Y=0 가정)에서 move만큼 위로 이동
        transform.localPosition = new Vector3(transform.localPosition.x, move, transform.localPosition.z);
    }
}
