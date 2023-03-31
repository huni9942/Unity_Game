using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // ** Ž�� ����
    public float scanRange;

    // ** Ÿ���� ���̾�
    public LayerMask targetLayer;

    // ** Ž�� ��� �迭
    public RaycastHit2D[] targets;

    // ** ���� ������ Ÿ��
    public Transform nearestTarget;

    void FixedUpdate()
    {
        // ** ���� �浹 ����
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        // ** ���� ������ Ÿ�� ���� 
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        
        // ** �÷��̾��� �ν� ����
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            // ** �÷��̾��� ��ġ
            Vector3 myPos = transform.position;

            // ** Ÿ���� ��ġ
            Vector3 targetPos = target.transform.position;

            // ** �÷��̾�� Ÿ�� ������ �Ÿ�
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}
