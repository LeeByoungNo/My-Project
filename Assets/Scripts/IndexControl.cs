using UnityEngine;
using UnityEngine.InputSystem;

public class IndexControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float rotationSpeed = 200f; // 회전 속도
    private Quaternion targetRotation;
    //private bool isRotating = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        // 부드럽게 목표 각도까지 회전
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 회전이 거의 완료되었는지 체크 (정밀도 보정)
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
        }
    }

    // ★ WebSocket 등 외부에서 이 함수를 호출하여 각도를 변경합니다.
    public void SetTargetRotation(float targetAngleY)
    {
        // 90, 180 같은 절대 수치를 기반으로 목표 쿼터니언 생성
        targetRotation = Quaternion.Euler(0, targetAngleY, 0);
    }
}
