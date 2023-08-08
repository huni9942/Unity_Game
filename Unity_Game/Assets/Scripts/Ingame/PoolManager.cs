using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ** �������� ������ �迭 ����
    public GameObject[] prefabs;

    // ** ������Ʈ Ǯ�� ������ ����Ʈ �迭 ����
    List<GameObject>[] pools;

    void Awake()
    {
        // ** ����Ʈ �迭 ���� �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        // ** ������Ʈ Ǯ ����Ʈ �ʱ�ȭ
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ** ��Ȱ�� ������ ���� ������Ʈ�� select ������ �Ҵ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ** ��Ȱ�� ������ ���� ������Ʈ �̹߰� ��, �����Ͽ� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}
