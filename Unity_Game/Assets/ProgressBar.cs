using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{

    private AsyncOperation asyncOperation;
    public Text text;
    public Text messagetext;
    public Image BlackImage;

    IEnumerator Start()
    {
        //EditorApplication.isPaused = true;
        asyncOperation = SceneManager.LoadSceneAsync("GameStart");
        asyncOperation.allowSceneActivation = false;
        float timer = 0.0f;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f) * 100f;
            text.text = progress + "%";

            timer += Time.deltaTime;
            if (asyncOperation.progress < 1.0f)
            {
                BlackImage.fillAmount = Mathf.Lerp(BlackImage.fillAmount, asyncOperation.progress, timer);
                if ( BlackImage.fillAmount >= asyncOperation.progress)
                {
                    timer = 0f;
                }
            }

            else
            {
                BlackImage.fillAmount = Mathf.Lerp(BlackImage.fillAmount, 1f, timer);
                if (BlackImage.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(1.0f);

                    messagetext.gameObject.SetActive(true);

                    if (Input.GetMouseButtonDown(0))
                        asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
