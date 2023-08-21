using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    // ** 경험치
    Rigidbody2D rigid;
    // ** 플레이어
    Rigidbody2D target;
    // ** 경험치 동전이 움직이는 속도
    float speed = 5.0f;
    // ** 자석 ON/OFF
    public bool mag;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        mag = false;
    }

    private void FixedUpdate()
    {
        // ** 자석 ON 상태일 때, 플레이어를 향해 이동
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
        // ** 플레이어와 충돌 시, 경험치 증가
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GetExp();
            gameObject.SetActive(false);
            mag = false;
        }
    }
}
