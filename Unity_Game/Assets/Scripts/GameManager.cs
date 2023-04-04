using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // ** 캐릭터 id
    public int playerId;

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

    // ** 게임 결과 ui
    public Result uiResult;

    // ** 몬스터 클리너
    public GameObject enemyCleaner;


    void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        // ** 임시 **
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();
    }

    public void GameOver()
    {
        // ** 게임 오버 시
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        // ** 게임 오버 시 반복
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        // ** 게임 오버 시
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        // ** 게임 오버 시 반복
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        // ** 재시작 시
        SceneManager.LoadScene(0);
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
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;

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
