using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ** Enemy�� ��ȯ ��ġ
    public Transform[] spawnPoint;

    // ** Enemy�� ��ȯ ����
    public SpawnData[] spawnData;

    // ** Enemy�� ��ȯ ����
    int level;

    // ** Enemy�� ��ȯ ����
    float timer;

    void Awake()
    {
        // ** Enemy�� ��ȯ ��ġ
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // ** ���� ��
        if (!GameManager.instance.isLive)
            return;

        // ** Enemy�� ��ȯ ���� �� ����
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
        // ** ��ȯ�� Enemy�� Ǯ���� ������
       GameObject enemy = GameManager.instance.pool.Get(0);

        // ** ��ȯ�� Enemy�� ��ġ
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        // ** ��ȯ�� Enemy�� ��ȯ ������ ���ڰ� ����
        enemy.GetComponent<EnemyController>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    // ** ��ȯ�� Enemy�� ���� �ð�
    public float spawnTime;

    // ** ��ȯ�� Enemy�� ��������Ʈ Ÿ��
    public int spriteType;

    // ** ��ȯ�� Enemy�� ü��
    public int health;

    // ** ��ȯ�� Enemy�� �ӵ�
    public float speed;
}