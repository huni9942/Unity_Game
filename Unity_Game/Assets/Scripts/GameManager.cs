using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        // ** �ӽ� **
        uiLevelUp.Select(0);
        isLive = true;
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
        }
    }

    public void GetExp()
    {
        exp++;

        // ** ���� �� ��
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
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
}
