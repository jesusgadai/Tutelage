﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Slider loadPercentage;

    int waitForSeconds;

    // #region Singleton
    // public static SceneController instance;
    // void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Debug.LogWarning("Multiple SceneController(s) found!");
    //         Destroy(this.gameObject);
    //     }

    //     instance = this;
    // }
    // #endregion 

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadScene(1);
            waitForSeconds = 1;
        }
        else
        {
            waitForSeconds = 0;
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        yield return new WaitForSeconds(waitForSeconds); // Delay for 1s to make sure splash scene is working properly
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone && loadPercentage != null)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadPercentage.value = progress;

            yield return null;
        }
    }
}
