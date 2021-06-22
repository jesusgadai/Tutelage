using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinballController : GameController
{
    public TMP_Text scoreValueText;
    public TMP_Text livesCountText;
    public Plunger plunger;
    public Hole hole;
    public int lives = 3;

    int multiplier = 1;
    int score = 0;

    void Start()
    {
        BumperHit[] bumpers = FindObjectsOfType<BumperHit>();
        MultiplierPad[] multiplierPads = FindObjectsOfType<MultiplierPad>();

        SetupBumpers(bumpers);
        SetupMultipliers(multiplierPads);

        hole.onBalLFell.AddListener(plunger.ResetBall);
        hole.onBalLFell.AddListener(SubtractLife);

        livesCountText.text = lives.ToString("00000");
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

    void SubtractLife()
    {
        lives--;
        livesCountText.text = lives.ToString("00000");

        if (lives <= 0)
        {
            GameDone();
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
