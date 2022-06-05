using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnalyser : MonoBehaviour
{
    public AudioSource source;
    public static float[] m_audioSpectrum;
    float[] tmp = new float[2048];
    public static float FFT;

    private void Update()
    {
        FFT = GetFFT();
    }

    private void Start()
    {
        m_audioSpectrum = new float[1024];
        
    }

    //Analyses the audio file, calculates the average and passes it back to beat detection.
    private float GetFFT()
    {
        float[] audioData = new float[1024];
        float x = 0;
        source.GetOutputData(audioData, 0);
        foreach(float s in audioData)
        {
            x += Mathf.Abs(s);
        }
        return x / audioData.Length;
    }
}