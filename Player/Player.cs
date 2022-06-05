using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private float health;
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private int playerID;
    [SerializeField]
    private PlayerArea area;
    [SerializeField]
    private bool hasMultiplier;

    public NoteDetector[] NoteDetectors { get => area.Detectors; set => area.Detectors = value; }
    public Transform[] NoteSpawnLocations { get => area.SpawnLocations; set => area.SpawnLocations = value; }
    public Transform NoteHolder { get => area.NoteHolder; set => area.NoteHolder = value; }
    public int PlayerID { get => playerID; set => playerID = value; }
    public float Health { get => health; set => health = value; }
    public float StartHealth { get => startHealth; set => startHealth = value; }
    public bool Multiplier { get => hasMultiplier; set => hasMultiplier = value; }

    public ControlScheme controlScheme;

    public enum Stance
    {
        ATTACKING,
        DEFENDING,
    }

    public Stance playerStance;
    public int comboMulti = 2;

    public void Awake()
    {
        foreach (NoteDetector noteDetector in NoteDetectors)
        {
            NoteDetector nd = noteDetector.GetComponent<NoteDetector>();
            nd.OwnedPlayerID = playerID;
        }
    }

    public void ChangeScore(int amount)
    {
        if (!Multiplier)
        {
            score += amount;

        }
        else
        {
            score += amount * comboMulti;
        }
    }

    public virtual void ChangeHealth(float amount)
    {
        health += amount;
    }

    public virtual void ChangeStance(Stance newStance)
    {
        playerStance = newStance;
    }

    public int GetScore()
    {
        return score;
    }

    public NoteDetector.NoteSpawn getRandomNoteSpawn()
    {
        int rand = Random.Range(0, NoteSpawnLocations.Length);
        NoteDetector.NoteSpawn spawn = new NoteDetector.NoteSpawn(NoteSpawnLocations[rand], NoteDetectors[rand]);
        return spawn;
    }

    protected virtual void Start()
    {
        health = startHealth;
    }

    public void SetupControlScheme(ControlScheme scheme)
    {
        controlScheme = scheme;
    }
}
