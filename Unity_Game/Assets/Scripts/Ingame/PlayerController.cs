using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** 움직일 벡터
    public Vector2 inputVec;

    // ** 이동 속도
    public float speed;

    // ** 탐지
    public Scanner scanner;

    // ** 손
    public HandController[] hands;

    // ** 애니메이터 컨트롤러
    public RuntimeAnimatorController[] animCon;

    // ** 플레이어의 Rigidbody2D
    Rigidbody2D rigid;

    // ** 플레이어의 SpriteRenderer
    SpriteRenderer spriter;

    // ** 플레이어의 Animator
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<HandController>(true);
    }

    private void OnEnable()
    {
        // ** 캐릭터에 따라 이동속도 변경
        speed *= CharacterController.Speed;

        // ** 캐릭터에 따라 애니메이션 변경
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void Update()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        // ** Input.GetAxisRaw = -1 ~ 1 사이의 값을 반환
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        // ** 입력받은 값으로 플레이어 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        // ** 플레이어가 바라보는 방향에 따라 이미지 반전
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ** 사망한 상태일 경우
        if (!GameManager.instance.isLive)
            return;

        // ** 피격 시
        GameManager.instance.health -= Time.deltaTime * 10;

        // ** 사망 시
        if (GameManager.instance.health < 0)
        {
            for (int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

}
