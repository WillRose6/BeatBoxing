using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : BeatReactor
{
    [SerializeField]
    private GameObject notePrefab;
    [SerializeField]
    private Transform noteHitLocation;
    [SerializeField]
    private float noteSpeed;

    public static float delay = 0;

    public float NoteSpeed { get => noteSpeed; set => noteSpeed = value; }

    private void Awake()
    {
        float distance = Vector2.Distance(PlayerManager.instance.Players[0].NoteSpawnLocations[0].position, noteHitLocation.position);
        delay = distance / noteSpeed;
    }

    public void SpawnNote()
    {
        foreach (Player p in PlayerManager.instance.Players)
        {
            NoteDetector.NoteSpawn spawn = p.getRandomNoteSpawn();
            GameObject note = Instantiate(notePrefab, p.NoteHolder.transform);
            note.transform.position = spawn.location.position;
            Note n = note.GetComponent<Note>();
            n.SetSpeed(noteSpeed);
            spawn.detector.AddNoteToColumn(n);
        }
    }

    public override void OnBeat()
    {
        base.OnBeat();
        SpawnNote();
    }
}


