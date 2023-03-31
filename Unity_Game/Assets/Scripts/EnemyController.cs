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

    // ** Enemy�� Collider2D�� �޾ƿ� ����
    Collider2D coll;

    // ** Enemy�� Animator�� �޾ƿ� ����
    Animator anim;

    // ** Enemy�� SpriteRenderer�� �޾ƿ� ����
    SpriteRenderer spriter;

    // ** ������ �ð�
    WaitForFixedUpdate wait;

    void Awake()
    {
        // ** Enemy�� Rigidbody2D�� �޾ƿ�
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy�� Collider2D�� �޾ƿ�
        coll = GetComponent<Collider2D>();

        // ** Enemy�� Animator�� �޾ƿ�
        anim = GetComponent<Animator>();

        // ** Enemy�� SpriteRenderer�� �޾ƿ�
        spriter = GetComponent<SpriteRenderer>();

        // ** ������ �ð� ����
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        // ** ����ϰų� Hit �ִϸ��̼� ��Ȳ�� ��
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        // ** Bullet�� �浹 ��, �������ŭ ü�� ����
        health -= collision.GetComponent<BulletController>().damage;
        StartCoroutine(KnockBack());

        // ** ���� ��, �ǰ� ó��
        if (health > 0)
        {
            // ** Hit �ִϸ��̼� ���
            anim.SetTrigger("Hit");
        }
        // ** ��� ��
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        // ** �÷��̾� ��ġ
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // ** �÷��̾� ���� �ݴ� ����
        Vector3 dirVec = transform.position - playerPos;
        // ** ���������� ���� ����
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
