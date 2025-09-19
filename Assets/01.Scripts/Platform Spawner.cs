using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefad; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판 수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최대값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y 값
    public float yMax = 1.5f; // 배치할 위치의 최대 y 값
    private float xPos = 20f; // 배치할 위치의 x 값
    void Start()
    {

    }

    void Update()
    {

    }
}
