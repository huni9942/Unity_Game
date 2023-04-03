using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    // ** 아이템 데이터
    public ItemData data;

    // ** 레벨
    public int level;

    // ** 무기
    public WeaponController weapon;

    // ** 장비
    public GearController gear;

    // ** 아이콘
    Image icon;

    // ** 레벨 텍스트
    Text textLevel;

    // ** 이름 텍스트
    Text textName;

    // ** 설명 텍스트
    Text textDesc;

    void Awake()
    {
        // ** 아이콘 스프라이트 불러오기
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        // ** 텍스트 불러오기
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    void OnEnable()
    {
        // ** 텍스트 변경
        // ** 레벨 텍스트
        textLevel.text = "Lv." + (level + 1);

        // ** 아이템 버튼 클릭 시 시행
        switch (data.itemType)
        {
            // ** 무기의 경우
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            // ** 그 외 장비의 경우
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }
    public void OnClick()
    {
        // ** 아이템 버튼 클릭 시 시행
        switch (data.itemType) 
        {
            // ** 무기의 경우
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                // ** 레벨 0일 때
                if (level == 0)
                {
                    // ** 새롭게 무기 오브젝트 생성
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponController>();
                    weapon.Init(data);
                }
                // ** 레벨이 0이 아닐 때
                else
                {
                    // ** 대미지 및 개수 증가
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                level++;
                break;
            // ** 그 외 장비의 경우
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                // ** level이 0일 때
                if (level == 0)
                {
                    // ** 새롭게 장비 오브젝트 생성
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<GearController>();
                    gear.Init(data);
                }
                // ** level이 0이 아닐 때
                else
                {
                    // ** 장비 관련 수치 증가
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }

                level++;
                break;
            // ** Heal의 경우
            case ItemData.ItemType.Heal:
                // ** 체력을 최대로 회복
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        // ** 레벨이 최대에 도달했을 때
        if (level == data.damages.Length)
        {
            // ** 버튼 off
            GetComponent<Button>().interactable = false;
        }
    }

}
