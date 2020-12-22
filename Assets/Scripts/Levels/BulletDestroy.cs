using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float delay = 2f;
    private bool exited = false;

    public delegate void ExitAction();
    public static event ExitAction OnFireExited;
    

    /* private void OnEnable()
     {
         Invoke("Destroy", 10f);
     }

     void Destroy()
     {
         gameObject.SetActive(false);
     }

     private void OnDisable()
     {
        CancelInvoke();
     }
    */

    
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
                Debug.Log("EXIT");
                Debug.Log("new one");
                //BulletFires.Fire();
                OnFireExited();


                StartCoroutine(WaitAndDeactivate());
                
           // }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("EXIT  2");
        transform.root.gameObject.SetActive(false);
        //gameObject.SetActive(false);
       // Destroy();
    }
  
   /* void Destroy()
    {
        gameObject.SetActive(false);
    }
   
    private void OnDisable()
    {
        CancelInvoke();
    }

   */
}
