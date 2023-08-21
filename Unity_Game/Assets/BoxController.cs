using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // ** 콜라이더
    Collider2D coll;
    // ** 애니메이션
    Animator anim;
    // ** 물리 제어
    Rigidbody2D rigid;
    // ** 스프라이트
    SpriteRenderer spriter;
    // ** 에니메이션 컨트롤러
    public RuntimeAnimatorController animCon;
    // ** 자석
    public GameObject mag;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        anim.runtimeAnimatorController = animCon;
    }

    void OnEnable()
    {
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        // ** 플레이어와 충돌 시, 애니메이션 시작 및 박스 오픈. 2초 후 소멸
        anim.SetTrigger("Open");
        OpenBox();
        Invoke("close", 2f);
    }

    void OpenBox()
    {
        // ** 박스 오픈 시, 자석 생성
        mag = GameManager.instance.pool.Get(6);
        mag.transform.position = gameObject.transform.position;
    }

    void close()
    {
        gameObject.SetActive(false);
    }
}
