using UnityEngine;

public class LiftSylinderControl : MonoBehaviour
{


    public bool isPushed = false;       // true면 밀고, false면 당깁니다.
    public float moveSpeed = 5f;        // 이동 속도
    public float pushDistance = 0.0065f;     // 밀려 나갈 거리

    Vector3 startPosition;              // 처음 위치 (당겨진 상태)
    Vector3 pushedPosition;             // 밀린 위치

    void Start()
    {
        // 시작할 때의 로컬 위치를 기준으로 두 지점을 설정합니다.
        startPosition = transform.localPosition;


        //pushedPosition = startPosition + new Vector3(0, 0, pushDistance);

        pushedPosition = startPosition + new Vector3(0, -pushDistance, 0);
    }

    void Update()
    {
        // 1. 매 프레임마다 현재 pushDistance 값을 반영하여 목표 지점을 실시간으로 계산합니다.
        // (아까 Y축의 마이너스 방향이 전진이라고 하셨으니 그대로 유지합니다)
        Vector3 currentPushedPos = startPosition + new Vector3(0, -pushDistance, 0);

        // 1. bool 값에 따라 목표 지점 결정
        Vector3 targetPos = isPushed ? currentPushedPos : startPosition;

        // 2. 부드럽게 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
    }

    // 외부(MQTT 등)에서 버튼처럼 호출할 때 사용
    public void TogglePush()
    {
        isPushed = !isPushed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌하면 일단 무조건 로그를 찍게 해봅니다.
        Debug.Log("무언가와 충돌함: " + other.gameObject.name);


        // 닿은 물체가 "Wafer" 태그를 가지고 있다면
        if (other.CompareTag("wafer"))
        {
            // 웨이퍼의 스크립트를 가져와서 나(실린더)를 부모로 설정하라고 명령
            Wafer wafer = other.GetComponent<Wafer>();
            if (wafer != null)
            {
                wafer.DetachFromTable(this.transform);
            }
        }
    }
}
