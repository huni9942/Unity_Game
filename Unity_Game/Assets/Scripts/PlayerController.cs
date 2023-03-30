using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** �������� ������ ����
    public Vector2 inputVec;

    // ** �����̴� �ӵ�
    public float speed;

    // ** �÷��̾��� Rigidbody2D�� �޾ƿ� ����
    Rigidbody2D rigid;

    // ** �÷��̾��� SpriteRenderer�� �޾ƿ� ����
    SpriteRenderer spriter;

    // ** �÷��̾��� Animator�� �޾ƿ� ����
    Animator anim;

    void Awake()
    {
        // ** �÷��̾��� Rigidbody2D�� �޾ƿ�
        rigid = GetComponent<Rigidbody2D>();

        // ** �÷��̾��� SpriteRenderer�� �޾ƿ�
        spriter = GetComponent<SpriteRenderer>();

        // ** �÷��̾��� Animator�� �޾ƿ�
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ** Input.GetAxisRaw = -1 ~ 1 ������ ���� ��ȯ
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // ** �Է¹��� ������ �÷��̾� �̵�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + inputVec);
    }

    private void LateUpdate()
    {
        // ** �÷��̾ �ٶ󺸴� ���⿡ ���� �̹��� ����
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

}
