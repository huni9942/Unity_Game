using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // ** 제목
    public GameObject[] titles;

    // ** 패배 시
    public void Lose()
    {
        titles[0].SetActive(true);
    }

    // ** 승리 시
    public void Win()
    {
        titles[1].SetActive(true);
    }
}
