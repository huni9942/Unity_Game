using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo("GameStart");
    }
}