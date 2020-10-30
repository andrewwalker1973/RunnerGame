using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    CoinDetector coinScript;

    // Start is called before the first frame update
    void Start()
    {
        coinScript = gameObject.GetComponent<CoinDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, coinScript.playerTransform.position,
            coinScript.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBubble")
        {
            //Add count or give points etc etc.
           // Destroy(gameObject);
        }
    }
}
