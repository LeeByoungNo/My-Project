using UnityEngine;

public class FeederControl : MonoBehaviour
{
    public bool isPushed = false;       // true면 밀고, false면 당깁니다.
    public float moveSpeed = 2f;  // 이동 속도
    public float moveDistance = 0.0065f; // 이동 거리 (중심에서 얼마나 멀리 갈지)

    Vector3 startPosition;
    Vector3 pushedPosition;             // 밀린 위치

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Feeder 스크립트가 시작되었습니다!");
        // 처음 시작 위치를 기억해둡니다.
        // 시작할 때의 로컬 위치를 기준으로 두 지점을 설정합니다.
        startPosition = transform.localPosition;

        pushedPosition = startPosition + new Vector3(0,0, -moveDistance);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPushedPos = startPosition + new Vector3(0,0 , -moveDistance);

        // 1. bool 값에 따라 목표 지점 결정
        Vector3 targetPos = isPushed ? currentPushedPos : startPosition;

        // 2. 부드럽게 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
    }
}
