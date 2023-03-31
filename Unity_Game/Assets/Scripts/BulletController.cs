using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ** Bullet�� �����
    public float damage;

    // ** Bullet�� ���� Ƚ��
    public int per;

    // ** Bullet�� Rigidbody2D�� �޾ƿ� ����
    Rigidbody2D rigid;

    void Awake()
    {
        // ** Bullet�� Rigidbody2D�� �޾ƿ�
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        // ** ������ ������ �ƴ� ���
        if (per > -1)
        {
            // ** �ӵ� ����
            rigid.velocity = dir * 15.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ** Tag�� Enemy�� �ƴϰų�, ������ ������ ���
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        // ������ -1�� �Ǿ��� ���
        if (per == -1)
        {
            // ** ���� �ӵ� �ʱ�ȭ
            rigid.velocity = Vector2.zero;
            // ** ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
