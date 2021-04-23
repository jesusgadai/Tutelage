using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinballController : GameController
{
    public TMP_Text scoreValueText;
    public TMP_Text timeValueText;
    public Plunger plunger;
    public Hole hole;
    public float timeLimitSeconds = 300;

    bool gameRunning;
    int multiplier = 1;
    int score = 0;

    void Start()
    {
        BumperHit[] bumpers = FindObjectsOfType<BumperHit>();
        MultiplierPad[] multiplierPads = FindObjectsOfType<MultiplierPad>();

        SetupBumpers(bumpers);
        SetupMultipliers(multiplierPads);

        hole.onBalLFell.AddListener(plunger.ResetBall);

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

    void SetupMultipliers(MultiplierPad[] multiplierPads)
    {
        foreach (MultiplierPad multiplierPad in multiplierPads)
        {
            multiplierPad.onTriggerEnter.AddListener(delegate { AddMultiplier(multiplierPad.multiplierValue); });
            multiplierPad.onMultiplierExpire.AddListener(delegate { SubtractMultiplier(multiplierPad.multiplierValue); });
        }
    }

    void AddScore(int score)
    {
        this.score += score;
        scoreValueText.text = this.score.ToString("00000");
    }

    void AddMultiplier(int multiplier)
    {
        this.multiplier += multiplier;
        Debug.Log("[PinballController.cs] - Multiplier: " + this.multiplier);
    }

    void SubtractMultiplier(int multiplier)
    {
        this.multiplier -= multiplier;
        Debug.Log("[PinballController.cs] - Multiplier: " + this.multiplier);
    }

    public override void GameDone()
    {
        base.GameDone();
    }
}
