using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wheel : MonoBehaviour
{
    private int randomvalue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;

    [SerializeField]
    private Text winText;


    private void Start()
    {
        coroutineAllowed = true;

    }

   /* private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && coroutineAllowed)
        {
            StartCoroutine(Spin());
        }
    }
*/


    public void SpinButton()
    {
        if (coroutineAllowed)
        {
            winText.text = "?";
            //finalAngle = 0;
            StartCoroutine(Spin());
        }
    }

    private IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomvalue = Random.Range(20, 30);
        timeInterval = 0.1f;


        for (int i = 0; i < randomvalue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt (randomvalue +0.5f))
            {
                timeInterval = 0.2f;
            }
            if (i > Mathf.RoundToInt(randomvalue * 0.85f))
            {
                timeInterval = 0.4f;
            }
            yield return new WaitForSeconds(timeInterval);
        }
       if (Mathf.RoundToInt (transform.eulerAngles.z) %45 !=0 )
        {
            transform.Rotate(0, 0, 22.5f);
            finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
            Debug.Log("finalAngle" + finalAngle);
        }
    
    
       switch (finalAngle)
        {
            case 0:
                winText.text = "Purple";
                Debug.Log("0");
                break;
            case 45:
                winText.text = "1";
                Debug.Log("1");
                break;
            case 90:
                winText.text = "2";
                Debug.Log("2");
                break;
            case 135:
                winText.text = "Black";
                Debug.Log("3");
                break;
            case 180:
                winText.text = "4";
                Debug.Log("4");
                break;
            case 225:
                winText.text = "Orange";
                Debug.Log("5");
                break;
            case 270:
                winText.text = "Red";
                Debug.Log("6");
                break;
            case 315:
                winText.text = "7";
                Debug.Log("7");
                break;


        }

        coroutineAllowed = true;

  }

}
