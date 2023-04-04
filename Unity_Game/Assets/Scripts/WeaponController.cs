using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // ** 무기 ID
    public int id;

    // ** 프리펩 ID
    public int prefabId;

    // ** 대미지
    public float damage;

    // ** 무기 개수
    public int count;

    // ** 무기 속도
    public float speed;

    // ** 시간
    float timer;

    // ** 플레이어
    PlayerController player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        // ** 정지 시
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            // ** 근접 무기의 경우
            case 0:
                // ** 무기 회전
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            // ** 원거리 무기의 경우
            default:
                timer += Time.deltaTime;
                // ** 연사 속도에 따라 총알 발사
                if (timer > speed)
                {
                    // ** 시간 초기화 및 발사
                    timer = 0.0f;
                    Fire();
                }

                break;
        }

    }

    public void LevelUp(float damage, int count)
    {
        // ** 레벨 업 시 대미지와 개수 증가
        this.damage = damage * CharacterController.Damage;
        this.count += count;

        // ** 근접 무기일 때
        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // ** 기본 세팅
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // ** 속성 세팅
        id = data.itemId;
        damage = data.baseDamage * CharacterController.Damage;
        count = data.baseCount + CharacterController.Count;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                // ** 회전 속도
                speed = 150 * CharacterController.WeaponSpeed;
                Batch();
                break;
            default:
                // ** 연사 속도
                speed = 0.5f * CharacterController.WeaponRate;
                break;
        }

        // ** 손 세팅
        HandController hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int index=0; index < count; index ++)
        {
            Transform bullet;
            
            // ** index가 현재 무기 개수보다 적을 때
            if (index < transform.childCount)
            {
                // ** index를 무기로 설정
                bullet = transform.GetChild(index);
            }
            else
            {
                // ** 가져올 무기의 transform을 저장
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                // ** 가져올 무기의 부모 변경
                bullet.parent = transform;
            } 
           
            // ** 무기의 위치, 회전 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // ** 무기 개수에 따라 각도 변경
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            // ** 무기 회전 및 이동
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 3.0f, Space.World);

            // ** 대미지, 관통 횟수 함수 호출(-1은 무한 관통)
            bullet.GetComponent<BulletController>().Init(damage, -1, Vector3.zero);
        }
    }
    void Fire()
    {
        // ** 근접한 타겟이 존재하지 않을 경우
        if (!player.scanner.nearestTarget)
            return;

        // ** 공격 목표를 가장 근접한 타겟으로 설정
        Vector3 targetPos = player.scanner.nearestTarget.position;
        // ** 투사체 방향이 공격 목표를 향하도록 설정
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<BulletController>().Init(damage, count, dir);
    }

}
