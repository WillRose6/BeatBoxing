using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarTesting : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    Debug.Log("Horizontal");
        //}
        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    Debug.Log("Vertical");
        //}
        //if (Input.GetAxis("Cross") != 0)
        //{
        //    Debug.Log("X");
        //}
        if (Input.GetAxis("Square") != 0)
        {
            Debug.Log("Square");
            Input.ResetInputAxes();
        }
        //if (Input.GetAxis("Triangle") != 0)
        //{
        //    Debug.Log("Triangle");
        //}
        //if (Input.GetAxis("Circle") != 0)
        //{
        //    Debug.Log("Circle");
        //}
        //if (Input.GetAxis("L1") != 0)
        //{
        //    Debug.Log("L1");
        //}
        //if (Input.GetAxis("R1") != 0)
        //{
        //    Debug.Log("R1");
        //}
        //if (Input.GetAxis("DPadHori") != 0)
        //{
        //    Debug.Log("left right");
        //}
        //if (Input.GetAxis("DPadVert") != 0)
        //{
        //    Debug.Log("up down");
        //}
        if (Input.GetAxis("Test") != 0)
        {
            Debug.Log("Found it");
        }
    }
}
