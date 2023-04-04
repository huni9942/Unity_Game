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

    // ** �ִϸ����� ��Ʈ�ѷ�
    public RuntimeAnimatorController[] animCon;

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

    private void OnEnable()
    {
        // ** ĳ���Ϳ� ���� �̵��ӵ� ����
        speed *= CharacterController.Speed;

        // ** ĳ���Ϳ� ���� �ִϸ��̼� ����
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void Update()
    {
        // ** ���� ��
        if (!GameManager.instance.isLive)
            return;

        // ** Input.GetAxisRaw = -1 ~ 1 ������ ���� ��ȯ
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // ** ���� ��
        if (!GameManager.instance.isLive)
            return;

        // ** �Է¹��� ������ �÷��̾� �̵�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // ** ���� ��
        if (!GameManager.instance.isLive)
            return;

        // ** �÷��̾ �ٶ󺸴� ���⿡ ���� �̹��� ����
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ** ����� ������ ���
        if (!GameManager.instance.isLive)
            return;

        // ** �ǰ� ��
        GameManager.instance.health -= Time.deltaTime * 10;

        // ** ��� ��
        if (GameManager.instance.health < 0)
        {
            for (int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

}
