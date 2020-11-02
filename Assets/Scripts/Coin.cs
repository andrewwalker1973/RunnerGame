using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    /*   private Animator anim;
       private GameObject _player;
       private readonly int _speed = 30;

       //public AudioClip CollectCoinSFX;
       //private SoundManager _soundManager;

       private void Start()
       {
          // _soundManager = GetComponent<SoundManager>();
       }

       private void Awake()
       {
           anim = GetComponent<Animator>();
       }

       private void Update()
       {
           if (_player != null)
           {
               print("coin moving");
               float step = _speed * Time.deltaTime;
               transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);

           }
       }

       private void OnEnable()
       {
           anim.SetTrigger("Spawn");
       }

       private void OnTriggerEnter(Collider other)
       {



           // activate when hit
           if (other.tag == "Player")
           {
               GameManager.Instance.GetCoin();
               anim.SetTrigger("Collected");

           }
       }

       public void Follow(GameObject player)
       {
           _player = player;
       }

       public void Collect()
       {
         //  _soundManager.PlaySFXClip(CollectCoinSFX);
           StartCoroutine(RemoveGameObject());
       }

       private IEnumerator RemoveGameObject()
       {
           yield return new WaitForSeconds(0.1f);
           Destroy(gameObject);
       }
    */

    public Transform playerTransform;
    public float moveSpeed = 17f;
   // private Animator anim;
    CoinMove coinMoveScript;

   /* private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetTrigger("Spawn");
    }
   */
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CoinDetector")
        {
            Debug.Log("Coin.cs trigger");
            coinMoveScript.enabled = true;
        }

        if (other.gameObject.tag == "PlayerBubble")
        {
            coinMoveScript.enabled = false;
        }

        if (other.tag == "Player")
        {
            GameManager.Instance.GetCoin();
          //  anim.SetTrigger("Collected");

        }
    }

}
