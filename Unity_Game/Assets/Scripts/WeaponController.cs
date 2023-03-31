using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // ** ���� ID
    public int id;

    // ** ������ ID
    public int prefabId;

    // ** �����
    public float damage;

    // ** ���� ����
    public int count;

    // ** ���� �ӵ�
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
                // ** ������ ������ transform�� ����
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                // ** ������ ������ �θ� ����
                bullet.parent = transform;
            } 
           
            // ** ������ ��ġ, ȸ�� �ʱ�ȭ
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // ** ���� ������ ���� ���� ����
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            // ** ���� ȸ�� �� �̵�
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // ** �����, ���� Ƚ�� �Լ� ȣ��(-1�� ���� ����)
            bullet.GetComponent<BulletController>().Init(damage, -1);
        }
    }
}
