using UnityEngine;
using UnityEngine.InputSystem;

public class WaferManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject waferPrefab; // 1단계에서 만든 프리팹을 여기에 연결할 겁니다.
    public Vector3 spawnPosition ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Space 바를 누르는 순간 실행
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnWafer();
        }
    }

    void SpawnWafer()
    {
        // Instantiate(오브젝트, 위치, 회전)
        // 프리팹을 spawnPoint의 위치와 회전값 그대로 생성합니다.
        Instantiate(waferPrefab, spawnPosition, Quaternion.identity);
    }

}
