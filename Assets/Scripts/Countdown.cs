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

    void Awake() {
        isCounting = true;
        pauseButton.onClick.AddListener(StopStart);
    }

    void Update()
    {
        if(isCounting)
        {
            timeLimitSeconds -= Time.deltaTime;

            if(timeLimitSeconds <= 0)
            {
                timeLimitSeconds = 0;
                isCounting = false;

                if(onCountdownComplete !=  null)
                onCountdownComplete.Invoke();
            }

            if(timeValueText != null)
                timeValueText.text = Mathf.Floor(timeLimitSeconds / 60).ToString("00") + ":" + Mathf.Floor(timeLimitSeconds % 60).ToString("00");
        }
    }

    public void StopStart()
    {
        isCounting = !isCounting;
    }
}
