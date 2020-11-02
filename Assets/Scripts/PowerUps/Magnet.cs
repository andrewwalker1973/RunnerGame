using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    //  public AudioClip CollectMagnetSFX;

    //  private SoundManager _soundManager;

    /*   void Start()
       {
      //     _soundManager = GetComponent<SoundManager>();
       }


       public void Collect()
       {
           // _soundManager.PlaySFXClip(CollectCoinSFX);
           Debug.Log("Magnet collect function");
           StartCoroutine(RemoveGameObject());
       }


       private IEnumerator RemoveGameObject()
       {
           yield return new WaitForSeconds(0.1f);
          // Destroy(gameObject);
       }
    */

    public GameObject coinDetectorobj;


    private void Start()
    {
        coinDetectorobj = GameObject.FindGameObjectWithTag("CoinDetector");
        coinDetectorobj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ;
        StartCoroutine(ActivateCoin());
        Destroy(transform.GetChild(0).gameObject);
    }

    IEnumerator ActivateCoin()
    {
        coinDetectorobj.SetActive(true);
        yield return new WaitForSeconds(4f);
        coinDetectorobj.SetActive(false);
    }
}
