using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coin":
                Debug.Log("Coin Collid");
                Coin coin = other.GetComponent<Coin>();
            //    coin.Follow(gameObject.transform.parent.gameObject);  
                break;
        }
    }
  
}
