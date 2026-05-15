using TMPro;
using UnityEngine;
using UnityEngine.UI; // 버튼 기능을 쓰기 위해 필수!
public class ButtonHandler : MonoBehaviour
{
    // 인스펙터에서 드래그해서 연결할 변수들
    public TMP_InputField myInputField;
    public Button myButton; // 인스펙터에서 버튼을 드래그해서 넣어주세요.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 버튼에 클릭 이벤트 리스너를 등록합니다.
        if (myButton != null)
        {
            myButton.onClick.AddListener(TaskOnClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        if (myInputField != null)
        {
            // .text 속성을 통해 입력된 값을 가져옵니다.
            string inputData = myInputField.text;

            Debug.Log("입력된 값: " + inputData);

            // 여기서 가져온 inputData를 활용해 물체를 이동시키거나 로직을 짜면 됩니다!
        }
    }
}
