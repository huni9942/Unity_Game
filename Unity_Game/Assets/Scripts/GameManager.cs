using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ** ���� �ð��� �޾ƿ� ����
    public float gameTime;

    // ** �ִ� ���� �ð��� �޾ƿ� ����
    public float maxgameTime = 2 * 10.0f;

    public PoolManager pool;
    public PlayerController player;

    void Awake()
    {
        instance = this;
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

}
