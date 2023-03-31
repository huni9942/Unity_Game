using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // ** 탐색 범위
    public float scanRange;

    // ** 타겟의 레이어
    public LayerMask targetLayer;

    // ** 탐색 결과 배열
    public RaycastHit2D[] targets;

    // ** 가장 근접한 타겟
    public Transform nearestTarget;

    void FixedUpdate()
    {
        // ** 원형 충돌 감지
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        // ** 가장 근접한 타겟 갱신 
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        
        // ** 플레이어의 인식 범위
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            // ** 플레이어의 위치
            Vector3 myPos = transform.position;

            // ** 타겟의 위치
            Vector3 targetPos = target.transform.position;

            // ** 플레이어와 타겟 사이의 거리
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
