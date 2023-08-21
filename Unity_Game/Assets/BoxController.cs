using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // ** �ݶ��̴�
    Collider2D coll;
    // ** �ִϸ��̼�
    Animator anim;
    // ** ���� ����
    Rigidbody2D rigid;
    // ** ��������Ʈ
    SpriteRenderer spriter;
    // ** ���ϸ��̼� ��Ʈ�ѷ�
    public RuntimeAnimatorController animCon;
    // ** �ڼ�
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
        // ** �÷��̾�� �浹 ��, �ִϸ��̼� ���� �� �ڽ� ����. 2�� �� �Ҹ�
        anim.SetTrigger("Open");
        OpenBox();
        Invoke("close", 2f);
    }

    void OpenBox()
    {
        // ** �ڽ� ���� ��, �ڼ� ����
        mag = GameManager.instance.pool.Get(6);
        mag.transform.position = gameObject.transform.position;
    }

    void close()
    {
        gameObject.SetActive(false);
    }
}
