using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]

    // ** 게임 시간
    public float gameTime;

    // ** 최대 게임 시간
    public float maxgameTime = 2 * 10.0f;

    [Header("# Player Info")]

    // ** 체력
    public int health;

    // ** 최대 체력
    public int maxHealth = 100;

    // ** 레벨
    public int level;

    // ** 킬 수
    public int kill;

    // ** 경험치
    public int exp;

    // ** 레벨별 필요 경험치 배열
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    
    // ** 풀
    public PoolManager pool;

    // ** 플레이어
    public PlayerController player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        // ** 게임 시간 설정
        gameTime += Time.deltaTime;

        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
