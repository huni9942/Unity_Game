using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ** 게임 시간을 받아올 변수
    public float gameTime;

    // ** 최대 게임 시간을 받아올 변수
    public float maxgameTime = 2 * 10.0f;

    public PoolManager pool;
    public PlayerController player;

    void Awake()
    {
        instance = this;
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

}
