using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagController : MonoBehaviour
{
    // ** 게임 내에 존재하는 경험치 동전들
    GameObject[] exps;
    // ** 경험치 동전의 스크립트
    ExpController expController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        exps = GameObject.FindGameObjectsWithTag("Exp");
        // ** 플레이어와 접촉 시, 자석 상태 ON. 3초 후 OFF
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.magOn = true;
            gameObject.SetActive(false);
            foreach (GameObject exp in exps)
            {
                expController = exp.GetComponent<ExpController>();
                expController.mag = true;
            }
            Invoke("MagOff", 3.0f);
        }
    }

    void MagOff()
    {
        GameManager.instance.magOn = false;
        expController.mag = false;
    }
}
