using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ** 프리펩을 저장할 배열 변수
    public GameObject[] prefabs;

    // ** 오브젝트 풀을 저장할 리스트 배열 변수
    List<GameObject>[] pools;

    void Awake()
    {
        // ** 리스트 배열 변수 초기화
        pools = new List<GameObject>[prefabs.Length];

        // ** 오브젝트 풀 리스트 초기화
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ** 비활성 상태의 게임 오브젝트를 select 변수에 할당
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ** 비활성 상태의 게임 오브젝트 미발견 시, 생성하여 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}
