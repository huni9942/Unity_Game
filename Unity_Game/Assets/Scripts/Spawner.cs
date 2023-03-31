using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ** Enemy의 소환 위치
    public Transform[] spawnPoint;

    // ** Enemy의 소환 정보
    public SpawnData[] spawnData;

    // ** Enemy의 소환 레벨
    int level;

    // ** Enemy의 소환 간격
    float timer;

    void Awake()
    {
        // ** Enemy의 소환 위치
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // ** Enemy의 소환 간격 및 레벨
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10.0f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // ** 소환될 Enemy를 풀에서 가져옴
       GameObject enemy = GameManager.instance.pool.Get(0);

        // ** 소환된 Enemy의 위치
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        // ** 소환된 Enemy에 소환 데이터 인자값 전달
        enemy.GetComponent<EnemyController>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    // ** 소환될 Enemy의 스폰 시간
    public float spawnTime;

    // ** 소환될 Enemy의 스프라이트 타입
    public int spriteType;

    // ** 소환될 Enemy의 체력
    public int health;

    // ** 소환될 Enemy의 속도
    public float speed;
}