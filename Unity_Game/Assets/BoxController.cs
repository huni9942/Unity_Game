using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Collider2D coll;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    public RuntimeAnimatorController animCon;

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

        anim.SetTrigger("Open");
        OpenBox();
        Invoke("close", 2f);
    }

    void OpenBox()
    {
        GameObject mag = GameManager.instance.pool.Get(6);
        mag.transform.position = gameObject.transform.position;
    }

    void close()
    {
        gameObject.SetActive(false);
    }
}
