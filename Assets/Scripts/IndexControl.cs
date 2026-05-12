using UnityEngine;
using UnityEngine.InputSystem;

public class IndexControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float rotationSpeed = 200f; // 회전 속도
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 'R' 키를 누르면 90도 회전 시작
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // 현재 각도에서 Y축 기준 90도를 더함
            targetRotation *= Quaternion.Euler(0, 90, 0);
            isRotating = true;
        }

        // 부드럽게 목표 각도까지 회전
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 회전이 거의 완료되었는지 체크
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            isRotating = false;
        }
    }
}
