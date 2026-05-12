using UnityEngine;

public class TrayMoveScript : MonoBehaviour
{

    public float moveSpeed = 0.1f;  // 이동 속도
    public float moveDistance = 1f; // 이동 거리 (중심에서 얼마나 멀리 갈지)

    //public GameObject loadItem; // 인스펙터에서 LoadItem을 드래그해서 연결

    public bool isLoaded = false;

    Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Tray 스크립트가 시작되었습니다!");
        // 처음 시작 위치를 기억해둡니다.
        startPosition = transform.position;

        //loadItem  = transform.Find("LoadItem").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        moveSpeed = 0.2f; 


        float minHeight = -0.4f;      // 최솟값
        float maxHeight = 0.4f;    // 최댓값
        float distance = maxHeight - minHeight; // 실제 이동할 거리 (5.5)

        // 0 ~ 5.5 사이를 왔다 갔다 하는 값 계산
        float move = Mathf.PingPong(Time.time * moveSpeed, distance);

        // 최솟값(1)에 move를 더하면 최종적으로 1 ~ 6.5 사이가 됩니다.
        float finalY = minHeight + move;

        // 현재 X, Z 좌표를 유지하면서 Y만 1~6.5 사이로 고정
        //transform.localPosition = new Vector3(transform.localPosition.x, finalY, transform.localPosition.z);

        //if(isLoaded)
        //{
        //    loadItem.SetActive(true); // 숨기기
        //}
        //else
        //{
        //    loadItem.SetActive(false); // 숨기기    
        //}
        
    }
}
