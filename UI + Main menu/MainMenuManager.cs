using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField]
    private SongChoiceScreen songChoiceScreen;
    [SerializeField]
    private CharacterSelectionScreen characterSelectionScreen;

    public MainMenuPlayer[] mainMenuPlayers;
    public ControlScheme[] controlSchemes;

    public List<Song> chosenSongs = new List<Song>();
    public Song[] songs;

    int playerOneSelection;
    int playerTwoSelection;
    int currentPage;

    private bool usingAI;

    public TextMeshProUGUI PlayerOneDerivative { get => characterSelectionScreen.playerOneDerivative; set => characterSelectionScreen.playerOneDerivative = value; }
    public TextMeshProUGUI PlayerTwoDerivative { get => characterSelectionScreen.playerTwoDerivative; set => characterSelectionScreen.playerTwoDerivative = value; }
    public TextMeshProUGUI PlayerOneControlSchemeText { get => characterSelectionScreen.playerOneControlSchemeText; set => characterSelectionScreen.playerOneControlSchemeText = value; }
    public TextMeshProUGUI PlayerTwoControlSchemeText { get => characterSelectionScreen.playerTwoControlSchemeText; set => characterSelectionScreen.playerTwoControlSchemeText = value; }
    public Text[] ChosenSongTexts { get => songChoiceScreen.chosenSongTexts; set => songChoiceScreen.chosenSongTexts = value; }
    public Text[] SongDisplayTexts { get => songChoiceScreen.songDisplayTexts; set => songChoiceScreen.songDisplayTexts = value; }
    public Transform SongHolder { get => songChoiceScreen.songHolder; set => songChoiceScreen.songHolder = value; }
    public bool UsingAI { get => usingAI; set => usingAI = value; }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("MainMenuManager already exists!");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        ConfigureInputsBasedOnNames();
        playerOneSelection = Array.IndexOf(controlSchemes, mainMenuPlayers[0].InputMode);
        playerTwoSelection = Array.IndexOf(controlSchemes, mainMenuPlayers[1].InputMode);
    }

    private void ConfigureInputsBasedOnNames()
    {
        string[] names = Input.GetJoystickNames();

        if (names.Length == 0)
        {
            mainMenuPlayers[0].InputMode = controlSchemes[0];
            PlayerOneDerivative.text = mainMenuPlayers[0].InputMode.derivation;
            PlayerOneControlSchemeText.text = mainMenuPlayers[0].InputMode.correspondingInputText;

            mainMenuPlayers[1].InputMode = controlSchemes[0];
            PlayerTwoDerivative.text = mainMenuPlayers[1].InputMode.derivation;
            PlayerTwoControlSchemeText.text = mainMenuPlayers[1].InputMode.correspondingInputText;
            GetNewConfigurationForPlayerTwo(false);

        }
        else if (names.Length == 1)
        {
            mainMenuPlayers[1].InputMode = controlSchemes[0];
            PlayerTwoDerivative.text = mainMenuPlayers[1].InputMode.derivation;
            PlayerTwoControlSchemeText.text = mainMenuPlayers[1].InputMode.correspondingInputText;
        }

        for (int i = 0; i < names.Length; i++)
        {
            mainMenuPlayers[i].InputMode = DecideController(names[i]);
            if (i == 0)
            {
                PlayerOneDerivative.text = mainMenuPlayers[i].InputMode.derivation;
                PlayerOneControlSchemeText.text = mainMenuPlayers[i].InputMode.correspondingInputText;
            }
            else
            {
                PlayerTwoDerivative.text = mainMenuPlayers[i].InputMode.derivation;
                PlayerTwoControlSchemeText.text = mainMenuPlayers[i].InputMode.correspondingInputText;
            }

        }
    }

    private ControlScheme DecideController(string name)
    {
        switch (name)
        {
            case "Guitar (Harmonix Guitar for Xbox 360)":
                return controlSchemes[2];

            case "Wireless Controller":
                return controlSchemes[1];

            case "":
                return controlSchemes[5];
        }

        return null;
    }

    public void GetNewConfigurationForPlayerOne(bool decrease)
    {
        string derivation = controlSchemes[playerOneSelection].derivation;
        do
        {
            if (!decrease)
            {
                if (playerOneSelection == controlSchemes.Length - 1)
                {
                    playerOneSelection = 0;
                }
                else
                {
                    playerOneSelection++;
                }
            }
            else
            {
                if (playerOneSelection == 0)
                {
                    playerOneSelection = controlSchemes.Length - 1;
                }
                else
                {
                    playerOneSelection--;
                }
            }
        } while (derivation != controlSchemes[playerOneSelection].derivation);

        mainMenuPlayers[0].InputMode = controlSchemes[playerOneSelection];
        PlayerOneControlSchemeText.text = mainMenuPlayers[0].InputMode.correspondingInputText;
    }

    public void GetNewConfigurationForPlayerTwo(bool decrease)
    {
        string derivation = controlSchemes[playerTwoSelection].derivation;
        do
        {
            if (!decrease)
            {
                if (playerTwoSelection == controlSchemes.Length - 1)
                {
                    playerTwoSelection = 0;
                }
                else
                {
                    playerTwoSelection++;
                }
            }
            else
            {
                if (playerTwoSelection == 0)
                {
                    playerTwoSelection = controlSchemes.Length - 1;
                }
                else
                {
                    playerTwoSelection--;
                }
            }
        } while (derivation != controlSchemes[playerTwoSelection].derivation);

        mainMenuPlayers[1].InputMode = controlSchemes[playerTwoSelection];
        PlayerTwoControlSchemeText.text = mainMenuPlayers[1].InputMode.correspondingInputText;
    }

    public void SetSongText(int id, Text t)
    {
        if (id < songs.Length)
        {
            t.text = songs[id].songName;
        }
        else
        {
            t.text = "???";
        }
    }

    public void AddSongToPool(int buttonID)
    {
        if (buttonID < songs.Length)
        {
            if (chosenSongs.Count < ChosenSongTexts.Length)
            {
                if (!CheckDuplicate(songs[buttonID]))
                {
                    chosenSongs.Add(songs[buttonID]);

                    for (int i = 0; i < chosenSongs.Count; i++)
                    {
                        ChosenSongTexts[i].text = chosenSongs[i].songName;
                    }

                    if (chosenSongs.Count == 2)
                    {
                        songChoiceScreen.ToggleContinueButton(true);
                    }
                }
            }
        }
    }

    public void RemoveSongFromPool(int buttonID)
    {
        if(chosenSongs.Count > 0)
        {
            if (buttonID < chosenSongs.Count)
            {
                chosenSongs.Remove(chosenSongs[buttonID]);
                ChosenSongTexts[buttonID].text = "";

                CleanupChosenSongText();

                for (int i = 0; i < chosenSongs.Count; i++)
                {
                    ChosenSongTexts[i].text = chosenSongs[i].songName;
                }

                songChoiceScreen.ToggleContinueButton(false);
            }
        }
    }

    private void CleanupChosenSongText()
    {
        foreach(Text t in ChosenSongTexts)
        {
            t.text = "";
        }
    }

    private bool CheckDuplicate(Song song)
    {
        foreach(Song s in chosenSongs)
        {
            if(song == s)
            {
                return true;
            }
        }

        return false;
    }

    private Song GetRandomSong()
    {
        Song songToUse = songs[0];
        do
        {
            songToUse = songs[UnityEngine.Random.Range(0, songs.Length)];
        } while (CheckDuplicate(songToUse));

        return songToUse;
    }

    public void LoadGame()
    {
        chosenSongs.Add(GetRandomSong());
        GameLoader.instance.OpenGameWithMusic(chosenSongs);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
