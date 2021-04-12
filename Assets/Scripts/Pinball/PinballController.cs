using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinballController : GameController
{
    public TMP_Text scoreValueText;
    public TMP_Text timeValueText;
    public Hole hole;
    public float timeLimitSeconds = 300;

    bool gameRunning;
    int score = 0;

    void Start()
    {
        BumperHit[] bumpers = FindObjectsOfType<BumperHit>();
        SetupBumpers(bumpers);

        hole.onBalLFell.AddListener(GameDone);

        gameRunning = true;
    }

    void Update()
    {
        if (gameRunning)
        {
            timeLimitSeconds -= Time.deltaTime;

            if (timeLimitSeconds <= 0)
            {
                timeLimitSeconds = 0;
                gameRunning = false;
            }

            timeValueText.text = Mathf.Floor(timeLimitSeconds / 60).ToString("00") + ":" + Mathf.Floor(timeLimitSeconds % 60).ToString("00");
        }
        else
        {
            GameDone();
        }
    }

    void SetupBumpers(BumperHit[] bumpers)
    {
        foreach (BumperHit bumper in bumpers)
        {
            bumper.onBumperHit.AddListener(delegate { AddScore(bumper.scoreValue); });
        }
    }

    void AddScore(int score)
    {
        this.score += score;
        scoreValueText.text = this.score.ToString("00000");
    }

    public override void GameDone()
    {
        base.GameDone();
    }
}
