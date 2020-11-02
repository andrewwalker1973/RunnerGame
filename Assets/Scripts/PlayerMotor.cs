using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

   // private const float LANE_DISTANCE = 2.5f; //set the lane width
    private const float LANE_DISTANCE = 1f; //set the lane width
    private const float TURN_SPEED = 0.05f;

    //
    private bool isRunning = false;

    //  Animation
    private Animator anim;
   Magnet magnet;
    // Movement
    private CharacterController controller;
    private float jumpForce = 4f; // WAS 4
    private float gravity = 12f; 
    private float verticalVelocity;
    
   
    private int desiredLane = 1; // 0=left 1=middle 2=right

    // speed Modifier
    private float originalSpeed = 9.0f; // was 7
    private float speed = 9.0f; // was 7
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    // added in code for power up process
    public enum PowerUpType { Magnet }
    public Dictionary<PowerUpType, PowerUp> powerUpDictionary;
    private float powerUpDuration = 10f;
    private List<PowerUpType> itemsToRemove;
    public GameObject Player;
    public GameObject MagnetCollider;



    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        magnet = gameObject.GetComponent<Magnet>();

        // Magnet stuff
     //   coinDetectorobj = GameObject.FindGameObjectWithTag("CoinDetector");
      //  coinDetectorobj.SetActive(false);
        coinMoveScript = gameObject.GetComponent<CoinMove>();


    }

    // added init for powerup process
    private void Awake()
    {
        // Create an empty dictionary and list, otherwise they'll be null later when we
        // try to access them and crash.
        powerUpDictionary = new Dictionary<PowerUpType, PowerUp>();
        itemsToRemove = new List<PowerUpType>();
     
    }


    private void Update()
    {

        




        if (!isRunning)
        {
            return; // if game is not started, dont run below code
        }
        
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            // change modifer text display
           GameManager.Instance.UpdateModifer(speed - originalSpeed);


        }

        // gather the inputs on which lane we should be in
   
        if (MobileInput.Instance.SwipeLeft )
        {
            MoveLane(false);
        }
        if (MobileInput.Instance.SwipeRight )
        {
            MoveLane(true);
        }

         if (Input.GetKeyDown(KeyCode.LeftArrow))
          {
            // move left
            MoveLane(false);
          }
          if (Input.GetKeyDown(KeyCode.RightArrow))
          {
              // move right
              MoveLane(true);
          }

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }


        // Calcuate move vector
        Vector3 moveVector = Vector3.zero;
       //  moveVector.x = (targetPosition - transform.position).normalized.x * speed; // character was shakaing 

        moveVector.x = (targetPosition - transform.position).x * speed;

        

        // Calc Y
        if (controller.isGrounded) //if grounded
        {
            //bool isGrounded = true;
            anim.SetBool("Grounded",true);

            verticalVelocity = -0.1f;
           

         //   if (Input.GetKeyDown(KeyCode.Space))
         if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
              

            }
            else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //slide
                StartSliding();
            }
        }
        else
        {
           // bool isGrounded = false;
            //  Debug.Log("NOTGrounded "); 
            verticalVelocity -= (gravity * Time.deltaTime); // slowly fall to ground level
          //  Debug.Log("Floating");
            // Fast Falling area
           // if (Input.GetKeyDown(KeyCode.M))
           if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                verticalVelocity = -jumpForce;  //drop immediatly to ground
                                                // Add code to drop quick and then slide
            //StartSliding();
               // anim.SetBool("DropSlide", true);

            }

            //code added for power up process
            foreach (KeyValuePair<PowerUpType, PowerUp> entry in powerUpDictionary)
            {
                entry.Value.Duration -= Time.deltaTime;


                // We can't remove an item from a dictionary if we're iterating through it.
                // Instead we have to keep track of it in a list and then remove the items from the
                // dictionary later.
               if (entry.Value.Duration <= 0)
                {
                    itemsToRemove.Add(entry.Key);
                }
         
            }


           // Go through all of the power-ups that need to be removed and remove it from the
            // dictionary.
            foreach (PowerUpType powerUpType in itemsToRemove)
            {
                switch (powerUpType)
                {
                    case PowerUpType.Magnet:
                        Transform magnetCollider = Player.transform.Find("Magnet Collider(Clone)");

                        print(magnetCollider);
                        Destroy(magnetCollider.gameObject);
                        magnetCollider = null;
                    break;
            }

            powerUpDictionary.Remove(powerUpType);
            }


            // We've removed everything, let's clear our list.
            itemsToRemove.Clear();
         

            /// end of code for powerups


        }



        moveVector.y = verticalVelocity;
        moveVector.z = speed;


        // move player
        //Debug.Log("Move Vecto" + moveVector);
        controller.Move(moveVector * Time.deltaTime);

        //Rotate charatcter in direction of travel
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
       {
            dir.y = 0;
             transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
            
        }
        
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }



    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
        
        // can add camera looking at something before game starts here\

    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);

        Invoke("StopSliding", 1.0f);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);

    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
                 }
    }


    // code for powerup triggger
    void OnTriggerEnter(Collider other)
    {
        print(other + " name " + other.name);
        switch (other.tag) {
           // case "Coin":
           //     CoinCollision(other);
           //     break;
            case "Magnet":
                 MagnetCollision(other);
                Debug.Log("Magnet collide");
               break;
            default:
                CheckUnTaggedCollision(other);
                break;
        }
    }


    //code added for powerups
    public void AddPowerUp(PowerUpType powerUpType)
    {
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
                Instantiate(MagnetCollider, Player.transform.position, Quaternion.identity, Player.transform);
                Debug.Log("Adding power up to player");
                //Try stop powerup
               StartCoroutine(RemoveMagnet());
                break;
        }

        // An interesting part of this is that if we get another power up that if we
        // get a duplicate power up, we will replace it with the new one.
       // ??  powerUpDictionary[powerUpType] = new PowerUp(powerUpDuration);
        
    }


    public bool ContainsPowerUp(PowerUpType powerUpType)
    {
        return powerUpDictionary.ContainsKey(powerUpType);
    }


    // Collides with the magnet, we add the power up to our list of power-ups
    // and let the power-up destroy itself from the game.
    /* private void MagnetCollision(Collider other)
     {
         AddPowerUp(PowerUpType.Magnet);
         Magnet magnet = other.GetComponent<Magnet>();
          magnet.Collect();
     }
    */

   // public GameObject coinDetectorobj;
    CoinMove coinMoveScript;
    private void MagnetCollision(Collider other)
    {
        AddPowerUp(PowerUpType.Magnet);
        if (other.gameObject.tag == "Player");
        StartCoroutine(ActivateCoin());
       // Destroy(transform.GetChild(0).gameObject);
    }

    IEnumerator ActivateCoin()
    {
        Debug.Log("Activate Coin");
      // coinDetectorobj.SetActive(true);
        yield return new WaitForSeconds(40f);
     //  coinDetectorobj.SetActive(false);
    }

    IEnumerator RemoveMagnet()
    {
        Debug.Log("Magnet active ");
        //coinDetectorobj.SetActive(true);
        yield return new WaitForSeconds(10f);
      //  coinMoveScript.enabled = true;
        Transform magnetCollider = Player.transform.Find("Magnet Collider(Clone)");

        print(magnetCollider);
        Destroy(magnetCollider.gameObject);
        magnetCollider = null;
    }
   
    // Check the collided object if it doesn't have a tag to see if it's
    // something we're also looking for.
    private void CheckUnTaggedCollision(Collider other)
{
        if (other.name.Contains("Cube"))
    {
                // TODO need to add something here
           // EnemyCollision();
        }
    }

}
