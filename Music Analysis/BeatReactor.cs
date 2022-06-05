using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReactor : MonoBehaviour
{
    public float bias;
    public float timeStep;

    private float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    public virtual void OnBeat()
    {
        timer = 0;
        isBeat = true;
    }

    public virtual void OnUpdate()
    {
        if (AudioAnalyser.FFT > bias)
        {
            if(timer > timeStep)
            {
                OnBeat();
            }
        }

        timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }
}