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

    // ** ���� ��Ÿ��
    float timer;

    // ** �÷��̾���Ʈ�ѷ� Ŭ���� ����
    PlayerController player;

    void Awake()
    {
        // ** �θ��� �÷��̾���Ʈ�ѷ� ������Ʈ�� �޾ƿ�
        player = GetComponentInParent<PlayerController>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        // ** id�� ���� ���� ���� ��� ����
        switch (id)
        {
            case 0:
                // ** ���� ȸ��
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0.0f;
                    Fire();
                }

                break;
        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
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
                // ** ȸ�� �ӵ�
                speed = 150;
                Batch();
                break;
            default:
                // ** ���� �ӵ�
                speed = 0.3f;
                break;
        }
    }

    void Batch()
    {
        for (int index=0; index < count; index ++)
        {
            Transform bullet;
            
            // ** index�� ���� ���� �������� ���� ��
            if (index < transform.childCount)
            {
                // ** index�� ����� ����
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
            bullet.GetComponent<BulletController>().Init(damage, -1, Vector3.zero);
        }
    }
    void Fire()
    {
        // ** ������ Ÿ���� �������� ���� ���
        if (!player.scanner.nearestTarget)
            return;

        // ** ���� ��ǥ�� ���� ������ Ÿ������ ����
        Vector3 targetPos = player.scanner.nearestTarget.position;
        // ** ����ü ������ ���� ��ǥ�� ���ϵ��� ����
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<BulletController>().Init(damage, count, dir);
    }

}
