using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI�� Ʈ������
    RectTransform rect;

    // ** ������ �迭
    ItemController[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemController>(true);
    }

    public void Show()
    {
        Next();
        // ** UI�� ���� ��
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        // ** UI�� ���� ��
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        // ** ������ Ŭ�� ��
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
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index=0; index < ran.Length; index++)
        {
            ItemController ranItem = items[ran[index]];

            // ** �ִ� ���� ���� ��, �Һ� ���������� ����
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
