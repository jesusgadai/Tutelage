using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Countdown : MonoBehaviour
{
    public Button pauseButton;
    public TMP_Text timeValueText;
    public float timeLimitSeconds;

    public UnityEvent onCountdownComplete;

    bool isCounting = false;
    float timeLimit;

    void OnEnable()
    {
        timeLimit = timeLimitSeconds;
        isCounting = true;
        pauseButton.onClick.AddListener(StopStart);
    }

    void Update()
    {
        if (isCounting)
        {
            timeLimit -= Time.deltaTime;

            if (timeLimit <= 0)
            {
                timeLimit = 0;
                isCounting = false;

                if (onCountdownComplete != null)
                    onCountdownComplete.Invoke();
            }

            if (timeValueText != null)
                timeValueText.text = Mathf.Floor(timeLimit / 60).ToString("00") + ":" + Mathf.Floor(timeLimit % 60).ToString("00");
        }
    }

    void OnDisable()
    {
        isCounting = false;
        gameObject.SetActive(false);
    }

    public void StopStart()
    {
        isCounting = !isCounting;
    }
}
