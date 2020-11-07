using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    Coin coinScript;
    //CoinMove coinMoveScript;

    //try fix coin not moving aay
  //  private GameObject objectToDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        coinScript = gameObject.GetComponent<Coin>();
      //  coinMoveScript = gameObject.GetComponent<CoinMove>();
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.position = Vector3.MoveTowards(transform.position, coinScript.playerTransform.position,
      //      coinScript.moveSpeed * Time.deltaTime);
        //Debug.Log("Should be moving coin");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBubble" || other.tag == "Player")
        {
            //Add count or give points etc etc.
            // Debug.Log("CoinMove.cs ");
            // GameManager.Instance.GetCoin();
            //Destroy(gameObject);
            // coinMoveScript.enabled = false;
          //  objectToDeactivate.SetActive(false);
        }

       // if (other.tag == "Player")
       // {
      //      GameManager.Instance.GetCoin();
            //  anim.SetTrigger("Collected");

      //  }
    }
}
