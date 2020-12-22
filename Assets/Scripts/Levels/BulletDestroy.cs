using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float delay = 60f;
    private bool exited = false;

    public delegate void ExitAction();
    public static event ExitAction OnFireExited;
    

        
    private void OnTriggerExit(Collider other)   // if the player tag cartag touches collider, mark it as no longer needed and deactivate
    {
        BulletFire BulletFires = gameObject.GetComponent<BulletFire>();

        CarTag carTag = other.GetComponent<CarTag>();
        if (carTag != null)
        {
            

          //  if (!exited)
        //   {
               exited = true;
                //     OnFireExited();
            ////    Debug.Log("EXIT");
            ////    Debug.Log("new one");
            
        StartCoroutine(WaitAndDeactivate());
          /////  OnFireExited();


               // StartCoroutine(WaitAndDeactivate());
         ////   transform.root.gameObject.SetActive(false);

            // }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("##########EXIT  2");
        Debug.Log("EXIT");
        Debug.Log("new one");

        StartCoroutine(WaitAndDeactivate());
        OnFireExited();


        // StartCoroutine(WaitAndDeactivate());
        transform.root.gameObject.SetActive(false);

        // transform.root.gameObject.SetActive(false);

    }
  
   
}
