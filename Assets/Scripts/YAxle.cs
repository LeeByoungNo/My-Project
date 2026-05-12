using UnityEngine;

public class YAxle : MonoBehaviour
{

    public float moveSpeed = 2f;  // 이동 속도
    public float moveDistance = 13f; // 이동 거리 (중심에서 얼마나 멀리 갈지)

    Vector3 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("YAxle 스크립트가 시작되었습니다!");
        // 처음 시작 위치를 기억해둡니다.
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        moveSpeed = 2f;
        moveDistance = 13f;

        // 0부터 moveDistance 사이를 왔다 갔다 하는 값을 계산합니다.
        float move = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        // 시작 위치에서 X축 방향으로 move만큼 더해줍니다.
        //transform.position = startPosition + new Vector3(move, 0, 0);
    }
}
