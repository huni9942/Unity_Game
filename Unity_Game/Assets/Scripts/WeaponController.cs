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

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }

    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for (int index=0; index < count; index ++)
        {
            Transform bullet;
            
            if (index < transform.childCount)
            {
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
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // ** 대미지, 관통 횟수 함수 호출(-1은 무한 관통)
            bullet.GetComponent<BulletController>().Init(damage, -1);
        }
    }
}
