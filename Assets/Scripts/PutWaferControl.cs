using UnityEngine;

public class PutWafer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌하면 일단 무조건 로그를 찍게 해봅니다.
        Debug.Log("무언가와 충돌함 in Bang: " + other.gameObject.name);


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
