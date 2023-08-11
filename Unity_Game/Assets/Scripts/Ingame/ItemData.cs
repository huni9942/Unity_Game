using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]

    // ** ������ Ÿ��
    public ItemType itemType;

    // ** ������ ID
    public int itemId;

    // ** ������ �̸�
    public string itemName;

    [TextArea]
    // ** ������ ����
    public string itemDesc;

    // ** ������ ������
    public Sprite itemIcon;

    [Header("# Level Data")]

    // ** �⺻ �����
    public float baseDamage;

    // ** �⺻ ����
    public int baseCount;

    // ** �����
    public float[] damages;

    // ** ����
    public int[] counts;

    [Header("# Weapon")]
    // ** �߻�ü
    public GameObject projectile;

    // ** ��
    public Sprite hand;
}