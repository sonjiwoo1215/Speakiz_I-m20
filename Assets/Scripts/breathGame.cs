using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string gameSceneName = "BreathGame"; // ���� ���� �̸�

    public void SwitchToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
