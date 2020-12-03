using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{


    // public Transform playerTransform;
    //   public float moveSpeed = 17f;
    // private Animator anim;
    //   CoinMove coinMoveScript;

    ////public AudioClip CollectCoinsSFX;
   // private SoundManager _soundManager;
    private GameObject _player;
    private readonly int _speed = 30;
    // private AudioSource source;
    private AudioManager audioManager;
    public string coinSoundName;
    // try fix coinnot leaving
    // private GameObject objectToDeactivate;


    

    private void Start()
    {
        
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

    }


    private void Update()
    {
        if (_player != null)
        {
          //  print("Coin moving");
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
        }
    }
       private void OnTriggerEnter(Collider other)
       {
            if (other.gameObject.tag == "RearCleanup")
            {
          //  Debug.Log("Rear Clean up");
                StartCoroutine(RemoveGameObject());
            }
       }

           
    private void OnEnable()
    {
        ///  anim.SetTrigger("Spawn");
        ///  // try remove game objects not collected
        ///  
        StartCoroutine(RemoveGameObject_not_collect());
    }

    public void Collect()
    {
        //Debug.Log("Coin Collected");
        //  _soundManager.PlaySFXClip(CollectCoinsSFX);
        audioManager.PlaySound(coinSoundName);
       //gameObject.SetActive(false);

        StartCoroutine(RemoveGameObject());
        
    }

    private IEnumerator RemoveGameObject()
    {
        yield return new WaitForSeconds(0.1f); // was 0.2
        Destroy(gameObject);
    }

    private IEnumerator RemoveGameObject_not_collect()
    {
        yield return new WaitForSeconds(40f);
        Debug.Log("Remove old coin");
        Destroy(gameObject);
    }

    public void Follow(GameObject player)
    {
        _player = player;
    }
}
