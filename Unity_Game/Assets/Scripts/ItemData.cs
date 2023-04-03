using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]

    // ** 아이템 타입
    public ItemType itemType;

    // ** 아이템 ID
    public int itemId;

    // ** 아이템 이름
    public string itemName;

    // ** 아이템 설명
    public string itemDesc;

    // ** 아이템 아이콘
    public Sprite itemIcon;

    [Header("# Level Data")]

    // ** 기본 대미지
    public float baseDamage;

    // ** 기본 개수
    public int baseCount;

    // ** 대미지
    public float[] damages;

    // ** 개수
    public int[] counts;

    [Header("# Weapon")]
    // ** 발사체
    public GameObject projectile;

    // ** 손
    public Sprite hand;
}
