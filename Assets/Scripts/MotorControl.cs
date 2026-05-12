using UnityEngine;

public class MotorControl : MonoBehaviour
{

    // 회전 속도를 조절할 변수 (나중에 PLC 데이터로 바꿀 수 있음)
    public float rotateSpeed = 500f;

    public bool isRunning = true; // 체크박스로 조절 가능!

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("모터 스크립트가 시작되었습니다!");
    }

    // Update is called once per frame
    void Update()
    {
        // 큐브를 매 프레임마다 Y축을 기준으로 회전시킵니다.
        // Time.deltaTime을 곱해야 컴퓨터 성능에 상관없이 일정한 속도로 돕니다.
        if (isRunning)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
            
    }
}
