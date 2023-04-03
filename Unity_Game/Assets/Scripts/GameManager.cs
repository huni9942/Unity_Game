using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    // ** 시간 정지 여부
    public bool isLive;

    // ** 게임 시간
    public float gameTime;

    // ** 최대 게임 시간
    public float maxgameTime = 2 * 10.0f;

    [Header("# Player Info")]

    // ** 체력
    public float health;

    // ** 최대 체력
    public float maxHealth = 100.0f;

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

    // ** 레벨업 ui
    public LevelUpController uiLevelUp;

    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        // ** 임시 **
        uiLevelUp.Select(0);
        isLive = true;
    }

    void Update()
    {
        // ** 정지 시
        if (!isLive)
            return;

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

        // ** 레벨 업 시
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        // ** 정지
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        // ** 재시작
        isLive = true;
        Time.timeScale = 1;
    }
}
