using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** ������ ����
    public Vector2 inputVec;

    // ** �̵� �ӵ�
    public float speed;

    // ** Ž��
    public Scanner scanner;

    // ** ��
    public HandController[] hands;

    // ** �÷��̾��� Rigidbody2D
    Rigidbody2D rigid;

    // ** �÷��̾��� SpriteRenderer
    SpriteRenderer spriter;

    // ** �÷��̾��� Animator
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<HandController>(true);
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
        rigid.MovePosition(rigid.position + nextVec);
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
