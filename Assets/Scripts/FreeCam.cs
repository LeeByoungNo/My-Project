using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCam : MonoBehaviour
{

    public float moveSpeed = 20f;
    public float lookSpeed = 0.1f; // 새로운 시스템에서는 값을 낮게 조정

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // 1. 회전 제어 (오른쪽 마우스 버튼 체크)
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            rotationY += mouseDelta.x * lookSpeed;
            rotationX -= mouseDelta.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        // 2. 이동 제어 (WASD 키 체크)
        Vector3 moveDir = Vector3.zero;
        var keyboard = Keyboard.current;

        if (keyboard.wKey.isPressed) moveDir += transform.forward;
        if (keyboard.sKey.isPressed) moveDir -= transform.forward;
        if (keyboard.aKey.isPressed) moveDir -= transform.right;
        if (keyboard.dKey.isPressed) moveDir += transform.right;

        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
    }
}
