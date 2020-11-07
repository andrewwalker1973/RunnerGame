using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    public GameObject Player;
   

    public void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // code for powerup triggger
    void OnTriggerEnter(Collider other)
    {
       print(other + " name " + other.name);
        switch (other.tag)
        {
             case "Coin":
                CoinCollision(other);
                 break;
          case "Magnet":
                MagnetCollision(other);
                Debug.Log("Magnet collide");
                break;
        
            default:
                CheckUnTaggedCollision(other);
                break;
        }
    }

    private void CoinCollision(Collider other)
    {
        Coin coin = other.GetComponent<Coin>();
        Debug.Log("Coin Collide");
       coin.Collect();
        GameManager.Instance.CollectCoin();
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

    private void MagnetCollision(Collider other)
    {

        Debug.Log("MagnetCollision");
        GameManager.Instance.AddPowerUp(GameManager.PowerUpType.Magnet);
        //  MagnetDestroy magnet1 = other.GetComponent<MagnetDestroy>();
        //     magnet1.Collect();
        //coinMoveScript = gameObject.GetComponent<CoinMove>();
        if (other.gameObject.tag == "Player") ;
       // StartCoroutine(ActivateCoin());

    }
}
