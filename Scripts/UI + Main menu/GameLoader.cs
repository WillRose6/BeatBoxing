using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public static GameLoader instance;
    private Character[] characters = new Character[2];
    public CharacterSelectionScreen characterSelectionScreen;
    public GameObject[] GameModes;
    private int usedByPlayer = 0;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("There is more than one GameLoader in the scene");
            return;
        }

        instance = this;
    }
    public void OpenGameWithMusic(List<Song> _songs)
    {
        AudioManager.songs = new Song[_songs.Count];
        for(int i = 0; i < _songs.Count; i++)
        {
            AudioManager.songs[i] = _songs[i];
        }

        SendCharacterPoolToGame();
        SceneManager.LoadScene(1);
    }

    public void SetGameMode(int modeID)
    {
        GameManager.gameMode = modeID;

        if(modeID == 1)
        {
            MainMenuManager.instance.UsingAI = true;
        }
    }

    public void SetPlayer(int playerID)
    {
        usedByPlayer = playerID;
    }

    public void SetCharacterInPool(Character character)
    {
        if (characters[usedByPlayer])
        {
            RemoveCharacterFromPool(usedByPlayer);
        }

        characters[usedByPlayer] = character;
        characterSelectionScreen.ShowCharacterDisplay(character, usedByPlayer);

        if (usedByPlayer == 0)
        {
            SetPlayer(1);
            if (MainMenuManager.instance.UsingAI)
            {
                SetCharacterInPool(characterSelectionScreen.characters[Random.Range(0,characterSelectionScreen.characters.Length)]);
            }
        }
    }

    public void RemoveCharacterFromPool(int playerID)
    {
        characterSelectionScreen.RemoveCharacter(playerID);
    }

    public void SendCharacterPoolToGame()
    {
        CharacterLoader.charsToLoad = characters;
    }
}
