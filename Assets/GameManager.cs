using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    public bool IsDead { set; get; }
    private PlayerMotor motor;


    //Try get start buttone to start
    private bool clickstart = false;

    // UI and UI Fields
    public Text scoreText, coinText, modifierText, hiScoreText; // convert to TMProd
    private float score, coinScore, modifierScore;
    public Animator gameCanvas;

    private int lastScore;

    // Death Menu
    public Animator deathMenuAnim, menuanim, menu_coinAnim;
    public Text deadscoreText, deadcoinText;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        //UPdateScores();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
       modifierText.text = "x" + modifierScore.ToString("0.0");
       scoreText.text = scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");

        //Debug - set score to be 0
       // PlayerPrefs.SetInt("HiScore", 0);
        // Get High Score
        hiScoreText.text = PlayerPrefs.GetInt("HiScore").ToString();


    }

    private void Update()
    {
        // if (MobileInput.Instance.Tap && !isGameStarted && clickstart) // try start with button click and not actual click
        if (!isGameStarted && clickstart)
        {
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuanim.SetTrigger("Hide");


        }

        if (isGameStarted && !IsDead)
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
        menu_coinAnim.SetTrigger("Blink");
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");

    }



    public void UpdateModifer(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }


    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }


    public void OnDeath()
    {
        IsDead = true;
        gameCanvas.SetTrigger("Hide");
        deadscoreText.text = score.ToString("0");
        deadcoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");


        //check if this is a high score
        if (score > PlayerPrefs.GetInt("HiScore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;

            PlayerPrefs.SetInt("HiScore", (int)s);
            // auth user to google
            PlayGamesController.AuthenticateUserFromgamemanager();
            // upload high score to google
            PlayGamesController.PostToLeaderboard((int)s);
        }
    }

    public void StartRun()
    {
        clickstart = true;
    }
}
