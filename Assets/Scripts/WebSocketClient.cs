using NativeWebSocket; // 라이브러리 설치 필요
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    // [중요] 이동시킬 YAxle 오브젝트를 인스펙터에서 연결하기 위한 변수
    public GameObject yAxle;
    public GameObject yTray;

    public GameObject liftBody;

    public GameObject inputFeeder;
    public GameObject outputFeeder;

    public LampController cs1;
    public LampController cs2;

    public LampController cs3;
    public LampController cs4;

    public LampController cs5;
    public LampController cs6;

    public TextMeshProUGUI debugText; // 2. 인스펙터에서 DebugOverlay를 연결할 변수

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
                string displayText = "";

                // 2. "D10"이라는 이름을 가진 키의 값을 가져옴
                if (data.ContainsKey("D10_DWORD"))
                {
                    float d10Value = (float)data["D10_DWORD"];

                    // 3. 마이크로미터 단위를 유니티 단위로 변환하여 이동
                    //float unityX = d10Value / 1000000f;
                    float unityX = d10Value / 100000000f;

                    unityX = unityX - 0.4f;
                    MoveYAxle(unityX);

                    displayText += "X :" + d10Value;
                }

                if (data.ContainsKey("D50_DWORD"))
                {
                    float d50Value = (float)data["D50_DWORD"];

                    // 3. 마이크로미터 단위를 유니티 단위로 변환하여 이동
                    //float unityX = d10Value / 1000000f;
                    //float unityY = d50Value / 10000000f;
                    float unityY = d50Value / 5000000f;

                    unityY = unityY - 0.21f;
                    MoveTray(unityY);

                    displayText += "\nY :" + d50Value;
                }

                if(data.ContainsKey("D76_DWORD"))
                {
                    float d76Value = (float)data["D76_DWORD"];

                    // 3. 마이크로미터 단위를 유니티 단위로 변환하여 이동
                    //float unityX = d10Value / 1000000f;
                    //float unityY = d50Value / 10000000f;
                    float unityY = d76Value / 5000000f;

                    
                    displayText += "\n3 :" + d76Value;
                }

                // 참고: X12 같은 불리언(True/False) 값 가져오기
                if (data.ContainsKey("X12"))
                {
                    bool isLampOn = (bool)data["X12"];
                    // 램프 제어 로직...
                }


                if (data.ContainsKey("X0"))
                {
                    int isLampOn = (int)data["X0"];
                    // Debug.Log("1 cs3 :"+ isLampOn);
                    // 램프 제어 로직...
                    cs1.lampOn = isLampOn == 1 ? true : false;
                }
                if (data.ContainsKey("X1"))
                {
                    int isLampOn = (int)data["X1"];
                    //Debug.Log("2 cs4 :" + isLampOn);
                    // 램프 제어 로직...
                    cs2.lampOn = isLampOn == 1 ? true : false;
                }

                if (data.ContainsKey("X2"))
                {
                    int isLampOn = (int)data["X2"];
                   // Debug.Log("1 cs3 :"+ isLampOn);
                    // 램프 제어 로직...
                    cs3.lampOn = isLampOn == 1 ? true : false;
                }
                if (data.ContainsKey("X3"))
                {
                    int isLampOn = (int)data["X3"];
                    //Debug.Log("2 cs4 :" + isLampOn);
                    // 램프 제어 로직...
                    cs4.lampOn = isLampOn == 1 ? true : false;
                }

                if (data.ContainsKey("X4"))
                {
                    int isLampOn = (int)data["X4"];
                    //Debug.Log("2 cs4 :" + isLampOn);
                    // 램프 제어 로직...
                    cs5.lampOn = isLampOn == 1 ? true : false;
                }
                if (data.ContainsKey("X5"))
                {
                    int isLampOn = (int)data["X5"];
                    //Debug.Log("2 cs4 :" + isLampOn);
                    // 램프 제어 로직...
                    cs6.lampOn = isLampOn == 1 ? true : false;
                }

                if (data.ContainsKey("Y10"))
                {
                    int isForwardOn = (int)data["Y10"];

                    inputFeeder.GetComponent<FeederControl>().isPushed = isForwardOn == 1 ? true : false;
                }

                if (data.ContainsKey("Y11"))
                {
                    int isForwardOn = (int)data["Y11"];

                    liftBody.GetComponent<LiftSylinderControl>().isPushed = isForwardOn == 1 ? true : false;
                }
                if (data.ContainsKey("Y12"))
                {
                    int isForwardOn = (int)data["Y12"];

                    outputFeeder.GetComponent<FeederControl>().isPushed = isForwardOn == 1 ? true : false;
                }

                // 3. 텍스트에 값 출력 (동적 입력)
                if (debugText != null)
                {
                    // 소수점 2자리까지 표시하거나 바코드 등 문자열 출력
                    debugText.text = displayText;
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
