using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    // ** Collider2D�� �޾ƿ� ����
    Collider2D coll;

    void Awake()
    {
        // ** Collider2D�� �޾ƿ�
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        // ** �÷��̾��� ��ġ
        Vector3 playerPos = GameManager.instance.player.transform.position;
        
        // ** ������Ʈ�� ��ġ
        Vector3 myPos = transform.position;

        // ** �÷��̾�� ������Ʈ ������ �Ÿ�
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // ** �÷��̾��� �̵� ����
        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        // ** tag�� ���� ������Ʈ ���ġ
        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + 
                        new Vector3(
                            Random.Range(-3.0f,3.0f), 
                            Random.Range(-3.0f, 3.0f),
                            0.0f));
                }
                break;
        }
    }
}
