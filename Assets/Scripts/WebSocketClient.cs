using UnityEngine;
using NativeWebSocket; // 라이브러리 설치 필요
using Newtonsoft.Json.Linq;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    // [중요] 이동시킬 YAxle 오브젝트를 인스펙터에서 연결하기 위한 변수
    public GameObject yAxle;
    public GameObject yTray;

    async void Start()
    {

        Debug.Log("WebSocketClient 스크립트가 시작되었습니다!");


        // 1. 서버 주소 설정 (예: 로컬 서버 또는 PLC 게이트웨이)
        websocket = new WebSocket("ws://localhost:8089/plc-stream?type=browser");

        // 2. 메시지 수신 시 이벤트 처리
        websocket.OnMessage += (bytes) =>
        {
            var jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("서버로부터 받은 메시지: " + jsonString);

            try
            {
                // 1. 문자열을 JSON 객체로 변환
                JObject data = JObject.Parse(jsonString);

                // 2. "D10"이라는 이름을 가진 키의 값을 가져옴
                if (data.ContainsKey("D10_DWORD"))
                {
                    float d10Value = (float)data["D10_DWORD"];

                    // 3. 마이크로미터 단위를 유니티 단위로 변환하여 이동
                    //float unityX = d10Value / 1000000f;
                    float unityX = d10Value / 10000000f;

                    unityX = unityX - 0.4f;
                    MoveYAxle(unityX);
                }

                if (data.ContainsKey("D50_DWORD"))
                {
                    float d50Value = (float)data["D50_DWORD"];

                    // 3. 마이크로미터 단위를 유니티 단위로 변환하여 이동
                    //float unityX = d10Value / 1000000f;
                    //float unityY = d50Value / 10000000f;
                    float unityY = d50Value / 5000000f;

                    unityY = unityY - 0.4f;
                    MoveTray(unityY);
                }

                // 참고: X12 같은 불리언(True/False) 값 가져오기
                if (data.ContainsKey("X12"))
                {
                    bool isLampOn = (bool)data["X12"];
                    // 램프 제어 로직...
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON 파싱 에러: " + e.Message);
            }
            
        };

        // 3. 서버 연결
        await websocket.Connect();
    }

    void MoveYAxle(float targetX)
    {
        if (yAxle != null)
        {
            // 현재 위치를 유지하면서 X값만 서버에서 온 값으로 변경합니다.
            Vector3 currentPos = yAxle.transform.localPosition;
            yAxle.transform.localPosition = new Vector3(targetX, currentPos.y, currentPos.z);
        }
    }
    void MoveTray(float targetY)
    {
        if (yTray != null)
        {
            // 현재 위치를 유지하면서 X값만 서버에서 온 값으로 변경합니다.
            Vector3 currentPos = yTray.transform.localPosition;
            yTray.transform.localPosition = new Vector3(currentPos.x, targetY, currentPos.z);
        }
    }

    void Update()
    {
        // [중요] 변수가 비어있지 않을 때만 실행하도록 안전 장치 추가
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            websocket.DispatchMessageQueue();
        }
    }

    private async void OnApplicationQuit()
    {
        // 종료 시에도 안전하게 닫기
        if (websocket != null)
        {
            await websocket.Close();
        }
    }
}
