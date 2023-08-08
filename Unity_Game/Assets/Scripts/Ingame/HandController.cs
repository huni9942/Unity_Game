using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // ** 왼손
    public bool isLeft;

    // ** 스프라이트
    public SpriteRenderer spriter;
    SpriteRenderer player;

    // ** 오른손 위치
    Vector3 rightPos = new Vector3(0.35f, 0.5f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, 0.5f, 0);

    // ** 왼손 회전
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    // ** 왼손 위치
    Vector3 leftPos = new Vector3(-0.4f, 0.35f, 0);
    Vector3 leftPosReverse = new Vector3(0.35f, 0.35f, 0);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        // ** 좌우 반전
        bool isReverse = player.flipX;

        // ** 근접 무기의 경우
        if (isLeft)
        {
            // ** 반전 상황에 따라 회전
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            transform.localPosition = isReverse ? leftPosReverse : leftPos;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        // ** 원거리 무기의 경우
        else
        {
            // ** 반전 상황에 따라 위치 이동
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
