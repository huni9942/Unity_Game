using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    // ** ����ġ
    Rigidbody2D rigid;
    // ** �÷��̾�
    Rigidbody2D target;
    // ** ����ġ ������ �����̴� �ӵ�
    float speed = 5.0f;
    // ** �ڼ� ON/OFF
    public bool mag;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        mag = false;
    }

    private void FixedUpdate()
    {
        // ** �ڼ� ON ������ ��, �÷��̾ ���� �̵�
        if (mag)
        {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        if (GameManager.instance.magOn)
            mag = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ** �÷��̾�� �浹 ��, ����ġ ����
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GetExp();
            gameObject.SetActive(false);
            mag = false;
        }
    }
}
