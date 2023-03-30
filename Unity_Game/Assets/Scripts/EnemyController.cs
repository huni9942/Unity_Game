using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy�� �ӵ�
    public float speed;

    // ** Enemy�� ������ Ÿ��
    public Rigidbody2D target;

    // ** Enemy�� ���� ����
    bool isLive = true;

    // ** Enemy�� Rigidbody2D�� �޾ƿ� ����
    Rigidbody2D rigid;

    // ** Enemy�� SpriteRenderer�� �޾ƿ� ����
    SpriteRenderer spriter;

    void Awake()
    {
        // ** Enemy�� Rigidbody2D�� �޾ƿ�
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy�� SpriteRenderer�� �޾ƿ�
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        // ** target�� ��ġ�� �����Ͽ� �̵�
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        // ** Enemy�� �ٶ󺸴� ��ġ�� ���� �̹��� ����
        spriter.flipX = target.position.x < rigid.position.x;
    }

}
