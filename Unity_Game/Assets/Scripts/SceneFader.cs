using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    // ** ���̵� ��/�ƿ� �� �̹���
    public Image img;
    // ** ���̵� ��/�ƿ� �� �ִϸ��̼� Ŀ��
    public AnimationCurve curve;

    private void Start()
    {
        // ** ���� �� ���̵��� �ڷ�ƾ ����
        StartCoroutine(FadeIn());
    }

    // ** �� �̵� �� ���̵� �ƿ� �ڷ�ƾ ����
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1.0f;

        // ** 1�� ���� �ִϸ��̼� Ŀ�꿡 ���� �̹����� ���� �������
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0.0f, 0.0f, 0.0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0.0f;

        // ** 1�� ���� �ִϸ��̼� Ŀ�꿡 ���� �̹����� ���� £������
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0.0f, 0.0f, 0.0f, a);
            yield return 0;
        }

        // ** ���� �ҷ��´�
        SceneManager.LoadScene(scene);
    }
}
