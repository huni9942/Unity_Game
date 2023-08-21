using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    // ** �ð� ���� ����
    public bool isLive;

    // ** ���� �ð�
    public float gameTime;

    // ** �ִ� ���� �ð�
    public float maxgameTime = 2 * 10.0f;

    [Header("# Player Info")]

    // ** ĳ���� id
    public int playerId;

    // ** ü��
    public float health;

    // ** �ִ� ü��
    public float maxHealth = 100.0f;

    // ** ����
    public int level;

    // ** ų ��
    public int kill;

    // ** ����ġ
    public int exp;

    // ** ����
    public GameObject item1;
    // ** �����
    public GameObject item2;

    // ** ������ �ʿ� ����ġ �迭
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    
    // ** Ǯ
    public PoolManager pool;

    // ** �÷��̾�
    public PlayerController player;

    // ** ������ ui
    public LevelUpController uiLevelUp;

    // ** ���� ��� ui
    public Result uiResult;

    // ** ���� Ŭ����
    public GameObject enemyCleaner;

    // ** �ڼ� ����
    public bool magOn;


    void Awake()
    {
        // ** ȭ�� ũ�� 900 * 900���� ���� �� ���� �Ұ�
        Screen.SetResolution(900, 900, false);
        instance = this;
        magOn = false;
    }

    public void GameStart(int id)
    {
        // ** ������ ĳ���� id
        playerId = id;
        // ** ���� �� �ִ� ü������ ����
        health = maxHealth;

        // ** �����ϻ簡 �ƴ� ��
        if (playerId != 2)
        {
            // ** ����� ��Ȱ��ȭ
            Destroy(item2);
            // ** �÷��̾� Ȱ��ȭ
            player.gameObject.SetActive(true);
            uiLevelUp.Select(playerId);
            Resume();
        }
        else
        {
            // ** �����ϻ� �� ��, ���� ��Ȱ��ȭ
            Destroy(item1);
            // ** �÷��̾� Ȱ��ȭ
            player.gameObject.SetActive(true);
            uiLevelUp.Select(playerId - 1);
            Resume();
        }
        

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        // ** ���� ���� �� ������ �۵�
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        // ** ��� 0.5�� ��, �й� UI Ȱ��ȭ �� ���� �Ͻ� ����
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
        // ** �¸��� �� ���� �۵�
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        // ** �¸� 0.5�� �� �¸� UI Ȱ��ȭ �� �Ͻ�����
        isLive = false;
        // ** Enemy �ʱ�ȭ
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        // ** ����� ��ư Ŭ�� ��, ���� Ȱ��ȭ ���� ���� �ٽ� �ҷ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // ** ���� ���� ���°� �ƴ� �� ��ȯ
        if (!isLive)
            return;

        // ** ���� �ð� ����
        gameTime += Time.deltaTime;

        // ** �ִ� ���� �ð����� ���� �������� ��� �¸�
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
            GameVictory();
        }
    }

    //** ����ġ
    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        // ** ���� �� �� ����ġ �ʱ�ȭ �� ������ ����
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // ** ����
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    // ** ���󺹱�
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
