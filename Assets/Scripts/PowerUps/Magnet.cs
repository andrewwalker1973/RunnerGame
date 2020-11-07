using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
     public AudioClip CollectMagnetSFX;

      private SoundManager _soundManager;

       void Start()
       {
          _soundManager = GetComponent<SoundManager>();
       }


     

   // public GameObject coinDetectorobj;


  /*  private void Start()
    {
        coinDetectorobj = GameObject.FindGameObjectWithTag("CoinDetector");
        coinDetectorobj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ;
        StartCoroutine(ActivateCoin());
        Debug.Log("transform.GetChild(0).gameObject" + transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(0).gameObject);
        // try get rif of magnet clone
       StartCoroutine(RemoveGameObject());
    }

    IEnumerator ActivateCoin()
    {
        coinDetectorobj.SetActive(true);
        yield return new WaitForSeconds(4f);
        coinDetectorobj.SetActive(false);
    }

   private IEnumerator RemoveGameObject()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Should delete magnet object");
         Destroy(gameObject);
    }
  */
    public void Collect()
    {
        // _soundManager.PlaySFXClip(CollectCoinSFX);
        Debug.Log("Magnet collect function");
        StartCoroutine(RemoveGameObject1());
    }


    private IEnumerator RemoveGameObject1()
    {
        yield return new WaitForSeconds(0.1f);
         Destroy(gameObject);
    }
}
