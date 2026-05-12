using UnityEngine;

public class Wafer : MonoBehaviour
{
    private bool isAttached = false;

    // Is Trigger가 체크된 영역에 들어갔을 때 실행됩니다.
    private void OnTriggerEnter(Collider other)
    {
        if (isAttached) return;

        // 테이블 중앙에 만든 영역(AttachZone)에 닿으면
        if (other.gameObject.name == "AttachZone")
        {
            AttachToTable(other.transform.parent); // 영역의 부모인 index_table을 부모로 설정
        }
        if (other.gameObject.name == "LiftBody")
        {
            Debug.Log("LiftBody OnTriggered....");                
        }
        if (other.gameObject.name == "OutPlane")
        {
            AttachToTable(other.transform); // 영역의 부모인 index_table을 부모로 설정
        }
    }

    void AttachToTable(Transform table)
    {
        isAttached = true;
        transform.SetParent(table);

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.detectCollisions = true;
        }
        Debug.Log("웨이퍼 안착 완료!");
    }

    // 이 함수를 호출하면 테이블에서 떨어져서 다시 집게(또는 실린더)를 따라갑니다.
    public void DetachFromTable(Transform lifter)
    {
        isAttached = false; // 다시 집을 수 있는 상태로 변경

        // 1. 부모를 다시 들어 올리는 장치(Lifter)로 변경
        transform.SetParent(lifter);

        // 2. 물리 설정 복구 (필요한 경우)
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;  // 들어 올릴 때는 물리 연산에 방해받지 않게 Kinematic 유지
            rb.detectCollisions = true; // 다시 충돌은 감지할 수 있게 변경
        }

        Debug.Log("웨이퍼 들어 올리기 완료!");
    }
}
