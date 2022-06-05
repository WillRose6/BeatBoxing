using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongChoiceScreen : MenuScreen
{
    public Text[] chosenSongTexts;
    public Text[] songDisplayTexts;
    public Transform songHolder;
    public Button continueButton;
    int songCount = 1;
    public GameObject[] SongPages;

    public void NextPageOfSongs()
    {
        songCount++;
        LoadPageOfSongs();
    }

    public void PreviousPageOfSongs()
    {
        if (songCount == 1)
        {
            songCount = songDisplayTexts.Length / 8;
        }
        else
        {
            songCount--;
        }
        LoadPageOfSongs();
    }

    public void LoadPageOfSongs()
    {
        if (songCount * 8 > songDisplayTexts.Length)
        {
            songCount = 1;
        }

        int a = songCount * 8;

        for (int i = a - 8; i < a; i++)
        {
            MainMenuManager.instance.SetSongText(i, songDisplayTexts[i]);
        }

        int pageNum = a / 8;

        foreach (GameObject g in SongPages)
        {
            g.SetActive(false);
        }
        SongPages[pageNum - 1].SetActive(true);
    }
    protected override void Start()
    {
        base.Start();
        LoadPageOfSongs();
    }

    public void ToggleContinueButton(bool toggle)
    {
        continueButton.interactable = toggle;
    }

}
