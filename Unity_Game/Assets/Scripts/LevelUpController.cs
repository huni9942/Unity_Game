using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI의 트랜스폼
    RectTransform rect;

    // ** 아이템 배열
    ItemController[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemController>(true);
    }

    public void Show()
    {
        Next();
        // ** UI가 열릴 때
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        // ** UI가 닫힐 때
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        // ** 아이템 클릭 시
        items[index].OnClick();
    }

    void Next()
    {
        // ** 아이템 비활성화
        foreach (ItemController item in items)
        {
            item.gameObject.SetActive(false);
        }

        // ** 랜덤 아이템 3개 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index=0; index < ran.Length; index++)
        {
            ItemController ranItem = items[ran[index]];

            // ** 최대 레벨 도달 시, 소비 아이템으로 변경
            if (ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }

}
