using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI의 트랜스폼
    RectTransform rect;

    // ** 아이템 배열
    public List<ItemController> items;

    public ItemController Weapon1;
    public ItemController Weapon2;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        items = new List<ItemController>(GetComponentsInChildren<ItemController>(true));
        rect = GetComponent<RectTransform>();
    }


    public void Show()
    {
        Next();
        // ** UI가 열릴 때
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        // ** UI가 닫힐 때
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        if (gameManager.playerId != 2)
            items.RemoveAt(2);
        else
            items.RemoveAt(1);

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
            ran[0] = Random.Range(0, items.Count);
            ran[1] = Random.Range(0, items.Count);
            ran[2] = Random.Range(0, items.Count);

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
