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

    // ** ȿ���� �ҽ� �迭
    public AudioSource[] sfxPlayer;

    // ** ȿ������ ������ �迭
    public AudioClip[] sfxClip;
    
    public enum Sfx { Select, LevelUp, Hit0, Hit1, Dead, Lose, Win, Melee0, Melee1, Range};

    // ** ����� ȿ����
    int sfxCursor;


    void Awake()
    {
        Screen.SetResolution(900, 900,false);
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();

        SfxPlay(Sfx.Select);
    }

    public void GameOver()
    {
        // ** ���� ���� ��
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        // ** ���� ���� �� �ݺ�
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        SfxPlay(Sfx.Lose);
    }

    public void GameVictory()
    {
        // ** ���� ���� ��
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        // ** ���� ���� �� �ݺ�
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        SfxPlay(Sfx.Win);
    }

    public void GameRetry()
    {
        // ** ����� ��
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        // ** ���� ��
        if (!isLive)
            return;

        // ** ���� �ð� ����
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

        // ** ���� �� ��
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();

            SfxPlay(Sfx.LevelUp);
        }
    }

    public void Stop()
    {
        // ** ����
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        // ** �����
        isLive = true;
        Time.timeScale = 1;
    }

    public void SfxPlay(Sfx type)
    {
        switch (type)
        {
            case Sfx.Dead:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.Hit0:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.Hit1:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.LevelUp:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.Lose:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                break;
            case Sfx.Melee0:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                break;
            case Sfx.Melee1:
                sfxPlayer[sfxCursor].clip = sfxClip[6];
                break;
            case Sfx.Range:
                sfxPlayer[sfxCursor].clip = sfxClip[7];
                break;
            case Sfx.Select:
                sfxPlayer[sfxCursor].clip = sfxClip[8];
                break;
            case Sfx.Win:
                sfxPlayer[sfxCursor].clip = sfxClip[9];
                break;
        }

        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }

}
