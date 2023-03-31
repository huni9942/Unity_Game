using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy의 속도
    public float speed;

    // ** Enemy의 체력
    public float health;

    // ** Enemy의최대 체력
    public float maxHealth;

    // ** Enemy의 애니메이션 컨트롤러
    public RuntimeAnimatorController[] animCon;

    // ** Enemy가 추적할 타겟
    public Rigidbody2D target;

    // ** Enemy의 생존 여부
    bool isLive;

    // ** Enemy의 Rigidbody2D를 받아올 변수
    Rigidbody2D rigid;

    // ** Enemy의 Animator를 받아올 변수
    Animator anim;

    // ** Enemy의 SpriteRenderer를 받아올 변수
    SpriteRenderer spriter;

    void Awake()
    {
        // ** Enemy의 Rigidbody2D를 받아옴
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy의 Animator를 받아옴
        anim = GetComponent<Animator>();

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
    void OnEnable()
    {
        // ** 추적할 target 설정
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        
        // ** 생존 여부 및 최대 체력 초기화
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        // ** Enemy의 정보를 Spawner 스크립트에서 받아옴
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        // ** Bullet과 충돌 시, 대미지만큼 체력 감소
        health -= collision.GetComponent<BulletController>().damage;

        // ** 생존 시, 피격 처리
        if (health > 0)
        {

        }
        // ** 사망 시
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
