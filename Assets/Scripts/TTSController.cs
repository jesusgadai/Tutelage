using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TTSController : MonoBehaviour
{
    const string languageCode = "en-US";
    [HideInInspector]
    public UnityEvent onSpeakStop;
    TextToSpeech ttsInstance;

    bool stopInvoked = false;


    void Start()
    {
        ttsInstance = TextToSpeech.instance;
        ttsInstance.onStartCallBack = OnSpeakStart;
        ttsInstance.onDoneCallback = OnSpeakStop;

        Setup(languageCode);
    }

    void Setup(string languageCode)
    {
        ttsInstance.Setting(languageCode, 1, 1);
    }

    public void StartSpeak(string message)
    {
        ttsInstance.StartSpeak(message);
    }

    public void ResetStopInvoked()
    {
        stopInvoked = false;
    }

    public void StopSpeak()
    {
        stopInvoked = true;
        ttsInstance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped...");
        if (onSpeakStop != null && !stopInvoked)
            onSpeakStop.Invoke();

        stopInvoked = false;
    }
}
