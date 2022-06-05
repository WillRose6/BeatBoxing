using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    [SerializeField]
    private NoteDetector[] detectors;
    [SerializeField]
    private Transform[] spawnLocations;
    [SerializeField]
    private Transform noteHolder;

    public NoteDetector[] Detectors { get => detectors; set => detectors = value; }
    public Transform[] SpawnLocations { get => spawnLocations; set => spawnLocations = value; }
    public Transform NoteHolder { get => noteHolder; set => noteHolder = value; }
}
