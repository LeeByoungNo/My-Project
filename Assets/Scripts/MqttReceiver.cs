using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;


public class MqttReceiver : MonoBehaviour
{
    private MqttClient client;

    // Unity의 메인 스레드에서 움직임을 처리하기 위한 변수
    public Transform xAxleObject; // 움직일 X축 오브젝트
    public Transform yAxleObject; // 움직일 Y축 오브젝트

    private float targetX = 0f;
    private float targetY = 0f;

    void Start()
    {
        // 포트번호와 프로토콜 버전을 명시적으로 지정합니다.
        client = new MqttClient("127.0.0.1", 1883, false, null, null, MqttSslProtocols.None);

        client.MqttMsgPublishReceived += OnMessageReceived;

        string clientId = Guid.NewGuid().ToString();

        // Connect 호출 시 프로토콜 버전을 3.1.1(버전에 따라 4)로 명시할 수 있습니다.
        // 만약 아래처럼 그냥 연결했을 때 에러가 난다면 브로커 설정을 확인해야 합니다.
        client.Connect(clientId);

        if (client.IsConnected)
        {
            Debug.Log("MQTT Connected!");
            // 구독 신청
            client.Subscribe(new string[] { "lamp/axis/x", "lamp/axis/y" },
                             new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }
    }

    // MQTT 수신 시 실행 (백그라운드 스레드)
    void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);

        //Debug.Log("현재 message: " + message);

        float value = float.Parse(message);

        if (e.Topic == "lamp/axis/x") targetX = value;
        else if (e.Topic == "lamp/axis/y") targetY = value;
    }

    void Update()
    {
        // 3. 실제 오브젝트 움직이기 (선형 보간으로 부드럽게 이동)
        Vector3 newPosProp = xAxleObject.localPosition;
        newPosProp.x = Mathf.Lerp(newPosProp.x, targetX * 0.001f, Time.deltaTime * 5f);
        xAxleObject.localPosition = newPosProp;

        Vector3 newPosAxis = yAxleObject.localPosition;
        newPosAxis.y = Mathf.Lerp(newPosAxis.y, targetY * 0.001f, Time.deltaTime * 5f);
        yAxleObject.localPosition = newPosAxis;
    }

    void OnApplicationQuit()
    {
        if (client != null && client.IsConnected) client.Disconnect();
    }
}
