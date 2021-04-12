using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent isGameDone;

    public virtual void GameDone()
    {
        Debug.Log("[GameController.cs] - Game Done");

        if (isGameDone != null)
            isGameDone.Invoke();
    }
}
