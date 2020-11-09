using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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


    // try for coins
    public int _coin = 0;

    //try for powerup
    // added in code for power up process
    public enum PowerUpType { Magnet }
    public Dictionary<PowerUpType, PowerUp> powerUpDictionary;
    private float powerUpDuration = 10f;
    private List<PowerUpType> itemsToRemove;
    public GameObject Player;
    public GameObject MagnetCollider;


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

        _coin = 0;


        //powerup
        // Create an empty dictionary and list, otherwise they'll be null later when we
        // try to access them and crash.
           powerUpDictionary = new Dictionary<PowerUpType, PowerUp>();
          itemsToRemove = new List<PowerUpType>();

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

            //powerup
            //code added for power up process
            foreach (KeyValuePair<PowerUpType, PowerUp> entry in powerUpDictionary)
            {
                entry.Value.Duration -= Time.deltaTime;
                Debug.Log("entry.Value.Duration" + entry.Value.Duration);


                // We can't remove an item from a dictionary if we're iterating through it.
                // Instead we have to keep track of it in a list and then remove the items from the
                // dictionary later.
                if (entry.Value.Duration <= 0)
                {
                    itemsToRemove.Add(entry.Key);
                    Debug.Log("duration ended");
                }

            }


            // Go through all of the power-ups that need to be removed and remove it from the
            // dictionary.
            foreach (PowerUpType powerUpType in itemsToRemove)
            {

                Debug.Log("foreach");

                switch (powerUpType)
                {
                    case PowerUpType.Magnet:
                        Debug.Log("in power up for loop");
                        Transform magnetCollider = Player.transform.Find("Magnet Collider(Clone)");
                        print(magnetCollider);
                        Destroy(magnetCollider.gameObject);
                        magnetCollider = null;
                        Debug.Log("Should remove magnet from list here");
                        break;
                }

                powerUpDictionary.Remove(powerUpType);
            }


            // We've removed everything, let's clear our list.
            itemsToRemove.Clear();


            /// end of code for powerups
        }
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
         //   PlayGamesController.AuthenticateUserFromgamemanager();
            // upload high score to google
         //   PlayGamesController.PostToLeaderboard((int)s);
        }
    }

    public void StartRun()
    {
        Debug.Log("Start Button Pressed.");
        clickstart = true;
    }

    public void CollectCoin()
    {
        _coin++;
        // pick up and score coins
            menu_coinAnim.SetTrigger("Blink");
              coinScore++;
              coinText.text = coinScore.ToString("0");
             score += COIN_SCORE_AMOUNT;
            scoreText.text = scoreText.text = score.ToString("0");
    }

    public int GetCoin()
    {
        return _coin;
    }

    //code added for powerups
    public void AddPowerUp(PowerUpType powerUpType)
    {

        Debug.Log("Adding in power up");
        switch (powerUpType)
        {
            case PowerUpType.Magnet:
                // if we already have the MagnetCollider, don't add it again.
                       if (powerUpDictionary.ContainsKey(powerUpType))
                         {
                              Debug.Log("Adding power up");
                              break;
                          }
                // We add the Magnet Collider to our player.
                Debug.Log("Createing objet");
               Instantiate(MagnetCollider, Player.transform.position, Quaternion.identity, Player.transform);
         //       Debug.Log("Adding power up to player");
                //Try stop powerup
          //      StartCoroutine(RemoveMagnet());
                break;
        }

        // An interesting part of this is that if we get another power up that if we
        // get a duplicate power up, we will replace it with the new one.
        Debug.Log("Poweruo dicto");
        powerUpDictionary[powerUpType] = new PowerUp(powerUpDuration);
        //  powerUpDictionary[powerUpType] = gameObject.AddComponent<PowerUp>();
    }


    public bool ContainsPowerUp(PowerUpType powerUpType)
    {
        return powerUpDictionary.ContainsKey(powerUpType);
    }

}
