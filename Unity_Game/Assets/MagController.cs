using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagController : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject[] exp;
    public bool mag;
    float speed = 5.0f;
    float timer;
    float waitingTime = 3.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        mag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timer = 0;
        timer += Time.deltaTime;
        if (collision.CompareTag("Player"))
        {
            mag = true;
            exp = GameObject.FindGameObjectsWithTag("Exp");
            if (timer > waitingTime)
                mag = false;
        }
        gameObject.SetActive(false);
    }
}
