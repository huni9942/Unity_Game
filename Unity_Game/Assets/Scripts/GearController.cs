using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    // ** 장비 타입
    public ItemData.ItemType type;

    // ** 장비 관련 수치
    public float rate;

    public void Init(ItemData data)
    {
        // ** 기본 세팅
        name = "Gear " + data.itemId;
        // ** 플레이어의 transform을 부모로 설정
        transform.parent = GameManager.instance.player.transform;
        // ** localPosition을 Reset
        transform.localPosition = Vector3.zero;

        // ** 속성 세팅
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        // ** 레벨 업 시 관련 수치 증가
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        // ** 장비 타입에 따른 함수 적용
        switch (type)
        {
            // ** 타입이 Glove 일 때, 연사력 증가
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            // ** 타입이 Shoe 일 때, 이동 속도 증가
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
            // ** 무기 별 관련 수치 증가
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
        // ** 플레이어 이동 속도 증가
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
