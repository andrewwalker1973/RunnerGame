﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTag : MonoBehaviour
{
    // Start is called before the first frame update



    private void OnCollisionEnter(Collision collision)
    {
         Debug.Log("collided with " + collision.gameObject.name);
    }

}
