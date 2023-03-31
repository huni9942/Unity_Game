using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy�� �ӵ�
    public float speed;

    // ** Enemy�� ü��
    public float health;

    // ** Enemy���ִ� ü��
    public float maxHealth;

    // ** Enemy�� �ִϸ��̼� ��Ʈ�ѷ�
    public RuntimeAnimatorController[] animCon;

    // ** Enemy�� ������ Ÿ��
    public Rigidbody2D target;

    // ** Enemy�� ���� ����
    bool isLive;

    // ** Enemy�� Rigidbody2D�� �޾ƿ� ����
    Rigidbody2D rigid;

    // ** Enemy�� Animator�� �޾ƿ� ����
    Animator anim;

    // ** Enemy�� SpriteRenderer�� �޾ƿ� ����
    SpriteRenderer spriter;

    void Awake()
    {
        // ** Enemy�� Rigidbody2D�� �޾ƿ�
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy�� Animator�� �޾ƿ�
        anim = GetComponent<Animator>();

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
    void OnEnable()
    {
        // ** ������ target ����
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        
        // ** ���� ���� �� �ִ� ü�� �ʱ�ȭ
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        // ** Enemy�� ������ Spawner ��ũ��Ʈ���� �޾ƿ�
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

}
