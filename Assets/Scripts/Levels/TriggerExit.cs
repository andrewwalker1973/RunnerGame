using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f; 

    public delegate void ExitAction();
    public static event ExitAction OnChunkFireExited;

    private bool exited = false;

    

    private void OnTriggerExit(Collider other)   // if the player tag cartag touches collider, mark it as no longer needed and deactivate
    {
        CarTag carTag = other.GetComponent<CarTag>();
        if (carTag != null)
        {
            Debug.Log("EXIT");
            if (!exited)
            {
                exited = true;
                OnChunkFireExited();
                StartCoroutine(WaitAndDeactivate());
            }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);
        transform.root.gameObject.SetActive(false);
    }

}
