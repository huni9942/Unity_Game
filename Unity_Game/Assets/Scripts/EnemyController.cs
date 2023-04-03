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

    // ** Enemy의 Collider2D를 받아올 변수
    Collider2D coll;

    // ** Enemy의 Animator를 받아올 변수
    Animator anim;

    // ** Enemy의 SpriteRenderer를 받아올 변수
    SpriteRenderer spriter;

    // ** 딜레이 시간
    WaitForFixedUpdate wait;

    void Awake()
    {
        // ** Enemy의 Rigidbody2D를 받아옴
        rigid = GetComponent<Rigidbody2D>();

        // ** Enemy의 Collider2D를 받아옴
        coll = GetComponent<Collider2D>();

        // ** Enemy의 Animator를 받아옴
        anim = GetComponent<Animator>();

        // ** Enemy의 SpriteRenderer를 받아옴
        spriter = GetComponent<SpriteRenderer>();

        // ** 딜레이 시간 생성
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        // ** 사망하거나 Hit 애니메이션 상황일 때
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        // ** target의 위치를 추적하여 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        // ** Enemy가 바라보는 위치에 따라 이미지 반전
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnEnable()
    {
        // ** 추적할 target 설정
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
        // ** Enemy의 정보를 Spawner 스크립트에서 받아옴
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        // ** Bullet과 충돌 시, 대미지만큼 체력 감소
        health -= collision.GetComponent<BulletController>().damage;
        StartCoroutine(KnockBack());

        // ** 생존 시, 피격 처리
        if (health > 0)
        {
            // ** Hit 애니메이션 출력
            anim.SetTrigger("Hit");
        }
        // ** 사망 시
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        // ** 플레이어 위치
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // ** 플레이어 기준 반대 방향
        Vector3 dirVec = transform.position - playerPos;
        // ** 순간적으로 힘을 가함
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
