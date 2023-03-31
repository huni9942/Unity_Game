using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** 움직임을 저장할 벡터
    public Vector2 inputVec;

    // ** 움직이는 속도
    public float speed;

    // ** Scanner 클래스 타입 변수
    public Scanner scanner;

    // ** 플레이어의 Rigidbody2D를 받아올 변수
    Rigidbody2D rigid;

    // ** 플레이어의 SpriteRenderer를 받아올 변수
    SpriteRenderer spriter;

    // ** 플레이어의 Animator를 받아올 변수
    Animator anim;

    void Awake()
    {
        // ** 플레이어의 Rigidbody2D를 받아옴
        rigid = GetComponent<Rigidbody2D>();

        // ** 플레이어의 SpriteRenderer를 받아옴
        spriter = GetComponent<SpriteRenderer>();

        // ** 플레이어의 Animator를 받아옴
        anim = GetComponent<Animator>();

        // ** Scanner 클래스를 받아옴
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        // ** Input.GetAxisRaw = -1 ~ 1 사이의 값을 반환
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // ** 입력받은 값으로 플레이어 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // ** 플레이어가 바라보는 방향에 따라 이미지 반전
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

}
