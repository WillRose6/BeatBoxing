using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laser1, laser2, laser3, laser4, laser5;
    Vector3 start1, start2, start3, start4, start5 = new Vector3(0.0f, 0.0f, 180.0f);
    Vector3 end1, end2, end3, end4, end5;

    public float timer;
    private float startTime;
    private bool loopin;
    bool fannedOut;

    private void Start()
    {
        fannedOut = true;
    }

    public void ResetPositions(float length)
    {
        end1 = new Vector3(0.0f, 0.0f, 180.0f);
        end2 = new Vector3(0.0f, 0.0f, 180.0f);
        end3 = new Vector3(0.0f, 0.0f, 180.0f);
        end4 = new Vector3(0.0f, 0.0f, 180.0f);
        end5 = new Vector3(0.0f, 0.0f, 180.0f);

        startTime = Time.time;
        timer = length;
    }

    public void FanOut(float length)
    {
        end1 = new Vector3(0.0f, 0.0f, 220.0f);
        end2 = new Vector3(0.0f, 0.0f, 200.0f);
        end3 = new Vector3(0.0f, 0.0f, 180.0f);
        end4 = new Vector3(0.0f, 0.0f, 160.0f);
        end5 = new Vector3(0.0f, 0.0f, 140.0f);

        startTime = Time.time;
        timer = length;
    }

    public void FocusIn(float length)
    {
        end1 = new Vector3(0.0f, 0.0f, 170.0f);
        end2 = new Vector3(0.0f, 0.0f, 175.0f);
        end3 = new Vector3(0.0f, 0.0f, 180.0f);
        end4 = new Vector3(0.0f, 0.0f, 185.0f);
        end5 = new Vector3(0.0f, 0.0f, 190.0f);

        startTime = Time.time;
        timer = length;
    }

    public void Lower(float length)
    {
        end1 = new Vector3(90.0f, 0.0f, 0.0f);
        end2 = new Vector3(90.0f, 0.0f, 0.0f);
        end3 = new Vector3(90.0f, 0.0f, 0.0f);
        end4 = new Vector3(90.0f, 0.0f, 0.0f);
        end5 = new Vector3(90.0f, 0.0f, 0.0f);

        startTime = Time.time;
        timer = length;
    }

    public void JustLoop()
    {
        loopin = true;
    }

    public void StopLooping()
    {
        loopin = false;
    }

    private void Update()
    {
        float fraction = (Time.time - startTime) / timer;

        laser1.transform.rotation = Quaternion.Euler(Vector3.Lerp(start1, end1, fraction));
        laser2.transform.rotation = Quaternion.Euler(Vector3.Lerp(start2, end2, fraction));
        laser3.transform.rotation = Quaternion.Euler(Vector3.Lerp(start3, end3, fraction));
        laser4.transform.rotation = Quaternion.Euler(Vector3.Lerp(start4, end4, fraction));
        laser5.transform.rotation = Quaternion.Euler(Vector3.Lerp(start5, end5, fraction));

        if (fraction >= 1)
        {
            start1 = end1;
            start2 = end2;
            start3 = end3;
            start4 = end4;
            start5 = end5;

            if (loopin)
            {
                if (fannedOut)
                {
                    FocusIn(3.0f);
                    fannedOut = false;
                }
                else
                {
                    FanOut(3.0f);
                    fannedOut = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.Z)) { JustLoop(); }

        
    }
}
