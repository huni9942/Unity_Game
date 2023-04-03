using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    // ** ��� Ÿ��
    public ItemData.ItemType type;

    // ** ��� ���� ��ġ
    public float rate;

    public void Init(ItemData data)
    {
        // ** �⺻ ����
        name = "Gear " + data.itemId;
        // ** �÷��̾��� transform�� �θ�� ����
        transform.parent = GameManager.instance.player.transform;
        // ** localPosition�� Reset
        transform.localPosition = Vector3.zero;

        // ** �Ӽ� ����
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        // ** ���� �� �� ���� ��ġ ����
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        // ** ��� Ÿ�Կ� ���� �Լ� ����
        switch (type)
        {
            // ** Ÿ���� Glove �� ��, ����� ����
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            // ** Ÿ���� Shoe �� ��, �̵� �ӵ� ����
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        WeaponController[] weapons = transform.parent.GetComponentsInChildren<WeaponController>();

        foreach(WeaponController weapon in weapons)
        {
            // ** ���� �� ���� ��ġ ����
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1.0f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        // ** �÷��̾� �̵� �ӵ� ����
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
