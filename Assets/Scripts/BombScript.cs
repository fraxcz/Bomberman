using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    DateTime StartingTime;
    void Start()
    {
        StartingTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = DateTime.Now - StartingTime;
        Debug.Log(ts.Seconds);
        if (ts.Seconds >= 5)
        {

            PlayerScript.BombPlaced = false;
            Destroy(gameObject);
        }
    }
}
