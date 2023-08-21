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

    // ** 소총
    public GameObject item1;
    // ** 기관총
    public GameObject item2;

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

    // ** 자석 상태
    public bool magOn;


    void Awake()
    {
        // ** 화면 크기 900 * 900으로 조정 및 변경 불가
        Screen.SetResolution(900, 900, false);
        instance = this;
        magOn = false;
    }

    public void GameStart(int id)
    {
        // ** 선택한 캐릭터 id
        playerId = id;
        // ** 시작 시 최대 체력으로 설정
        health = maxHealth;

        // ** 전문하사가 아닐 시
        if (playerId != 2)
        {
            // ** 기관총 비활성화
            Destroy(item2);
            // ** 플레이어 활성화
            player.gameObject.SetActive(true);
            uiLevelUp.Select(playerId);
            Resume();
        }
        else
        {
            // ** 전문하사 일 시, 소총 비활성화
            Destroy(item1);
            // ** 플레이어 활성화
            player.gameObject.SetActive(true);
            uiLevelUp.Select(playerId - 1);
            Resume();
        }
        

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        // ** 게임 오버 할 때마다 작동
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        // ** 사망 0.5초 후, 패배 UI 활성화 및 게임 일시 정지
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.Lose);

    }

    public void GameVictory()
    {
        // ** 승리할 때 마다 작동
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        // ** 승리 0.5초 후 승리 UI 활성화 및 일시정지
        isLive = false;
        // ** Enemy 초기화
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        // ** 재시작 버튼 클릭 시, 현재 활성화 중인 씬을 다시 불러옴
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // ** 게임 진행 상태가 아닐 시 반환
        if (!isLive)
            return;

        // ** 게임 시간 설정
        gameTime += Time.deltaTime;

        // ** 최대 게임 시간보다 오래 생존했을 경우 승리
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
            GameVictory();
        }
    }

    //** 경험치
    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        // ** 레벨 업 시 경험치 초기화 및 아이템 선택
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // ** 정지
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    // ** 원상복귀
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
