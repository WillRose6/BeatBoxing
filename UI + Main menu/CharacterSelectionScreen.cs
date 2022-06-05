using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionScreen : MenuScreen
{
    public Transform[] characterSpawnLocations;
    private List<GameObject> spawnedCharacters = new List<GameObject>();
    public TMPro.TextMeshProUGUI[] chosenCharacterTexts;
    public TMPro.TextMeshProUGUI playerOneDerivative;
    public TMPro.TextMeshProUGUI playerTwoDerivative;
    public TMPro.TextMeshProUGUI playerOneControlSchemeText;
    public TMPro.TextMeshProUGUI playerTwoControlSchemeText;
    public Button continueButton;
    public Character[] characters;

    public void ShowCharacterDisplay(Character character, int playerID)
    {
        chosenCharacterTexts[playerID].SetText(character.characterName);
        GameObject c = Instantiate(character.mainMenuPrefab, characterSpawnLocations[playerID].position, characterSpawnLocations[playerID].rotation);
        spawnedCharacters.Add(c);
        MainMenuManager.instance.mainMenuPlayers[playerID].character = character;

        if (spawnedCharacters.Count == 2)
        {
            EnableContinue();
        }
    }

    public void HideCharacterDisplay()
    {
        for(int i = 0; i < spawnedCharacters.Count; i++)
        {
            Destroy(spawnedCharacters[i]);
        }
    }

    public void RemoveCharacter(int playerID)
    {
        Destroy(spawnedCharacters[playerID]);
        spawnedCharacters.RemoveAt(playerID);
        MainMenuManager.instance.mainMenuPlayers[playerID].character = null;
    }

    public override void ChangeScreen()
    {
        base.ChangeScreen();
        HideCharacterDisplay();
        GameManager.loadedInPlayers = MainMenuManager.instance.mainMenuPlayers;
    }

    protected override void Start()
    {
        base.Start();
    }

    public void EnableContinue()
    {
        continueButton.interactable = true;
    }
}
