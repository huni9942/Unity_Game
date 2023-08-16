using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI의 트랜스폼
    RectTransform rect;

    // ** 아이템 배열
    public List<ItemController> items;

    // ** 소총
    public ItemController Weapon1;
    // ** 기관총
    public ItemController Weapon2;

    // ** 게임 매니저
    public GameManager gameManager;

    void Awake()
    {
        // ** 해당 오브젝트의 컴포넌트 가져오기
        gameManager = gameManager.GetComponent<GameManager>();
        items = new List<ItemController>(GetComponentsInChildren<ItemController>(true));
        rect = GetComponent<RectTransform>();
    }

    // ** UI가 열릴 때
    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    // ** UI가 닫힐 때
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    // ** 선택 시
    public void Select(int index)
    {
        // ** 전문하사가 아닐 때는 소총 삭제, 전문하사일 경우 기관총 삭제
        if (gameManager.playerId != 2)
            items.RemoveAt(2);
        else
            items.RemoveAt(1);

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
            ran[0] = Random.Range(0, items.Count-1);
            ran[1] = Random.Range(0, items.Count-1);
            ran[2] = Random.Range(0, items.Count-1);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for (int index = 0; index < ran.Length; index++)
        {
            ItemController ranItem = items[ran[index]];

            // ** 아이템 레벨이 최대가 아닐 경우, 해당 아이템 활성화
            if (ranItem.level != ranItem.data.damages.Length)
                ranItem.gameObject.SetActive(true);
            else
                foreach (ItemController item in items)
                {
                    if (item != items[4] && item.level != item.data.damages.Length)
                    {
                        item.gameObject.SetActive(true);
                        break;
                    }
                    else if (item != items[4] && item.level == item.data.damages.Length)
                    {

                    }
                    else
                        items[4].gameObject.SetActive(true);
                }
        }
    }
}
