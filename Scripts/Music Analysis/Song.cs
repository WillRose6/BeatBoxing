using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Music/Song", order = 1)]
public class Song : ScriptableObject
{
    public AudioClip clip;
    public float speed;
    public float bias;
    public float timeStep;
    public string songName;
}
