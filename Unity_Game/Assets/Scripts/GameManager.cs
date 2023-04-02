using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]

    // ** ���� �ð�
    public float gameTime;

    // ** �ִ� ���� �ð�
    public float maxgameTime = 2 * 10.0f;

    [Header("# Player Info")]

    // ** ü��
    public int health;

    // ** �ִ� ü��
    public int maxHealth = 100;

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

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
