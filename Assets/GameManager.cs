﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private PlayerMotor motor;
   

    // UI and UI Fields
    public Text scoreText, coinText, modifierText; // convert to TMProd
    private float score, coinScore, modifierScore;

    private int lastScore;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        //UPdateScores();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");


    }

    private void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();

        }

        if (isGameStarted)
        {
            //Increase the score
            
            score += (Time.deltaTime * modifierScore); // gain points for the time you are running
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
            
        }
    }

    public void GetCoin()
    {
        // pick up and score coins
        coinScore ++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");

    }

  

    public void UpdateModifer(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

}
