using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // ** �޼�
    public bool isLeft;

    // ** ��������Ʈ
    public SpriteRenderer spriter;
    SpriteRenderer player;

    // ** ������ ��ġ
    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);

    // ** �޼� ȸ��
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        // ** �¿� ����
        bool isReverse = player.flipX;

        // ** ���� ������ ���
        if (isLeft)
        {
            // ** ���� ��Ȳ�� ���� ȸ��
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        // ** ���Ÿ� ������ ���
        else
        {
            // ** ���� ��Ȳ�� ���� ��ġ �̵�
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
