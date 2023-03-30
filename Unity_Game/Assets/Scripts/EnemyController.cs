using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy의 속도
    public float speed;

    // ** Enemy가 추적할 타겟
    public Rigidbody2D target;

    // ** Enemy의 생존 여부
    bool isLive = true;

    // ** Enemy의 Rigidbody2D를 받아올 변수
    Rigidbody2D rigid;

    // ** Enemy의 SpriteRenderer를 받아올 변수
    SpriteRenderer spriter;

    void Awake()
    {
        // ** Enemy의 Rigidbody2D를 받아옴
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy의 SpriteRenderer를 받아옴
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        // ** target의 위치를 추적하여 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        // ** Enemy가 바라보는 위치에 따라 이미지 반전
        spriter.flipX = target.position.x < rigid.position.x;
    }

}
