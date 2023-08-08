using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ** Bullet의 대미지
    public float damage;

    // ** Bullet의 관통 횟수
    public int per;

    // ** Bullet의 Rigidbody2D를 받아올 변수
    Rigidbody2D rigid;

    void Awake()
    {
        // ** Bullet의 Rigidbody2D를 받아옴
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        // ** 관통이 무한이 아닐 경우
        if (per > -1)
        {
            // ** 속도 적용
            rigid.velocity = dir * 15.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ** Tag가 Enemy가 아니거나, 관통이 무한일 경우
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        // 관통이 -1이 되었을 경우
        if (per == -1)
        {
            // ** 물리 속도 초기화
            rigid.velocity = Vector2.zero;
            // ** 비활성화
            gameObject.SetActive(false);
        }
    }
}
