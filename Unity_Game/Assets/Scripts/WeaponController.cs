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

    // ** �ð�
    float timer;

    // ** �÷��̾�
    PlayerController player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        // ** ���� ��
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            // ** ���� ������ ���
            case 0:
                // ** ���� ȸ��
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            // ** ���Ÿ� ������ ���
            default:
                timer += Time.deltaTime;
                // ** ���� �ӵ��� ���� �Ѿ� �߻�
                if (timer > speed)
                {
                    // ** �ð� �ʱ�ȭ �� �߻�
                    timer = 0.0f;
                    Fire();
                }

                break;
        }

    }

    public void LevelUp(float damage, int count)
    {
        // ** ���� �� �� ������� ���� ����
        this.damage = damage * CharacterController.Damage;
        this.count += count;

        // ** ���� ������ ��
        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // ** �⺻ ����
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // ** �Ӽ� ����
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
                // ** ȸ�� �ӵ�
                speed = 150 * CharacterController.WeaponSpeed;
                Batch();
                break;
            default:
                // ** ���� �ӵ�
                speed = 0.5f * CharacterController.WeaponRate;
                break;
        }

        // ** �� ����
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
            bullet.Translate(bullet.up * 3.0f, Space.World);

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

        AudioManager.instance.PlayeSfx(AudioManager.Sfx.Range);

    }

}
