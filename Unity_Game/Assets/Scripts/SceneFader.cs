using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    // ** 페이드 인/아웃 용 이미지
    public Image img;
    // ** 페이드 인/아웃 시 애니메이션 커브
    public AnimationCurve curve;

    private void Start()
    {
        // ** 시작 시 페이드인 코루틴 실행
        StartCoroutine(FadeIn());
    }

    // ** 씬 이동 시 페이드 아웃 코루틴 실행
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1.0f;

        // ** 1초 동안 애니메이션 커브에 따라 이미지가 점차 사라진다
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

        // ** 1초 동안 애니메이션 커브에 따라 이미지가 점차 짙어진다
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0.0f, 0.0f, 0.0f, a);
            yield return 0;
        }

        // ** 씬을 불러온다
        SceneManager.LoadScene(scene);
    }
}
