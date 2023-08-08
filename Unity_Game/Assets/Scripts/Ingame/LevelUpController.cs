using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    // ** UI�� Ʈ������
    RectTransform rect;

    // ** ������ �迭
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
        // ** UI�� ���� ��
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlayeSfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        // ** UI�� ���� ��
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
            ran[0] = Random.Range(0, items.Count);
            ran[1] = Random.Range(0, items.Count);
            ran[2] = Random.Range(0, items.Count);

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
