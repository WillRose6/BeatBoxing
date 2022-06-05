using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private ScoreManager scoreManager;
    public ScoreManager ScoreManager { get { return scoreManager; } set { scoreManager = value; } }

    [SerializeField]
    private PlayerManager playerManager;
    public PlayerManager PlayerManager { get => playerManager; set => playerManager = value; }

    [SerializeField]
    private UI ui;
    public UI Ui { get => ui; set => ui = value; }

    [SerializeField]
    private AudioSource musicSource;
    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    [SerializeField]
    private GameObject[] gameModes;
    public static int gameMode;

    [SerializeField]
    private ControlScheme[] controlSchemes;

    public static MainMenuPlayer[] loadedInPlayers;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("GameManager already exists!");
            return;
        }

        instance = this;
        LoadInPlayers();
    }

    private void Start()
    {
        if (!ScoreManager)
        {
            Debug.LogError("Score manager could not be found!");
        }
        if (!playerManager)
        {
            Debug.LogError("Player manager could not be found!");
        }

        gameModes[gameMode].gameObject.SetActive(true);
    }

    private void StartSong()
    {
        musicSource.Play();
    }

    public void LoadInPlayers()
    {
        playerManager.Players[0].SetupControlScheme(loadedInPlayers[0].InputMode);
        playerManager.Players[1].SetupControlScheme(loadedInPlayers[1].InputMode);
    }
}
