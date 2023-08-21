using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagController : MonoBehaviour
{
    // ** ���� ���� �����ϴ� ����ġ ������
    GameObject[] exps;
    // ** ����ġ ������ ��ũ��Ʈ
    ExpController expController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        exps = GameObject.FindGameObjectsWithTag("Exp");
        // ** �÷��̾�� ���� ��, �ڼ� ���� ON. 3�� �� OFF
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
