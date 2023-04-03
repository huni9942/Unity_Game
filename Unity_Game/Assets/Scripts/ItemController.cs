using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    // ** ������ ������
    public ItemData data;

    // ** ����
    public int level;

    // ** ����
    public WeaponController weapon;

    // ** ���
    public GearController gear;

    // ** ������
    Image icon;

    // ** ���� �ؽ�Ʈ
    Text textLevel;

    // ** �̸� �ؽ�Ʈ
    Text textName;

    // ** ���� �ؽ�Ʈ
    Text textDesc;

    void Awake()
    {
        // ** ������ ��������Ʈ �ҷ�����
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        // ** �ؽ�Ʈ �ҷ�����
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    void OnEnable()
    {
        // ** �ؽ�Ʈ ����
        // ** ���� �ؽ�Ʈ
        textLevel.text = "Lv." + (level + 1);

        // ** ������ ��ư Ŭ�� �� ����
        switch (data.itemType)
        {
            // ** ������ ���
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            // ** �� �� ����� ���
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
        // ** ������ ��ư Ŭ�� �� ����
        switch (data.itemType) 
        {
            // ** ������ ���
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                // ** ���� 0�� ��
                if (level == 0)
                {
                    // ** ���Ӱ� ���� ������Ʈ ����
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponController>();
                    weapon.Init(data);
                }
                // ** ������ 0�� �ƴ� ��
                else
                {
                    // ** ����� �� ���� ����
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                level++;
                break;
            // ** �� �� ����� ���
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                // ** level�� 0�� ��
                if (level == 0)
                {
                    // ** ���Ӱ� ��� ������Ʈ ����
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<GearController>();
                    gear.Init(data);
                }
                // ** level�� 0�� �ƴ� ��
                else
                {
                    // ** ��� ���� ��ġ ����
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }

                level++;
                break;
            // ** Heal�� ���
            case ItemData.ItemType.Heal:
                // ** ü���� �ִ�� ȸ��
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        // ** ������ �ִ뿡 �������� ��
        if (level == data.damages.Length)
        {
            // ** ��ư off
            GetComponent<Button>().interactable = false;
        }
    }

}
