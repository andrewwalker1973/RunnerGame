using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 40f);
       // Debug.Log("&&&&&&&&&&&&&&&Destroy cuse not used");

    }

 /*   // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect()
    {
        // _soundManager.PlaySFXClip(CollectCoinSFX);
        Debug.Log("######################Collect Called");
        StartCoroutine(RemoveGameObject());
    }


    private IEnumerator RemoveGameObject()
    {
        yield return new WaitForSeconds(10f);
        //Destroy(gameObject);
        Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$$Destroy magnet");
        Destroy(transform.GetChild(0).gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger on Magmet");
        if (other.gameObject.tag == "Player")
        {
            Collect();
        }
    }
 */
}
