using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp
{
    // getter and setter for changing the value of the Power Up's duration.
    public float Duration
    {
        get; set;
    }


    // Constructor that takes in the duration of the power up.
    public PowerUp(float duration)
    {
        Duration = duration;
    }

}
