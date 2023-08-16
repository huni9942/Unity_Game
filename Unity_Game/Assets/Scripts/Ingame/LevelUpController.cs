using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI�� Ʈ������
    RectTransform rect;

    // ** ������ �迭
    public List<ItemController> items;

    // ** ����
    public ItemController Weapon1;
    // ** �����
    public ItemController Weapon2;

    // ** ���� �Ŵ���
    public GameManager gameManager;

    void Awake()
    {
        // ** �ش� ������Ʈ�� ������Ʈ ��������
        gameManager = gameManager.GetComponent<GameManager>();
        items = new List<ItemController>(GetComponentsInChildren<ItemController>(true));
        rect = GetComponent<RectTransform>();
    }

    // ** UI�� ���� ��
    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    // ** UI�� ���� ��
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    // ** ���� ��
    public void Select(int index)
    {
        // ** �����ϻ簡 �ƴ� ���� ���� ����, �����ϻ��� ��� ����� ����
        if (gameManager.playerId != 2)
            items.RemoveAt(2);
        else
            items.RemoveAt(1);

        items[index].OnClick();
    }

    void Next()
    {
        // ** ������ ��Ȱ��ȭ
        foreach (ItemController item in items)
        {
            item.gameObject.SetActive(false);
        }
        

        // ** ���� ������ 3�� Ȱ��ȭ
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

            // ** ������ ������ �ִ밡 �ƴ� ���, �ش� ������ Ȱ��ȭ
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
