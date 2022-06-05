using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum eScreen
    {
        MAIN,
        SONGCHOICE,
        CHARACTERSELECT,
        PRESSSTART,
    }

    public MenuScreen[] Screens;
    public Animator mainMenuAnimator;

    public void OpenScreen(eScreen newScreen)
    {
        DisableAllScreens();

        foreach(MenuScreen s in Screens)
        {
            s.ChangeScreen();
            //if(s.screenType == newScreen)
            //{
            //    s.gameObject.SetActive(true);
            //}
        }

        switch (newScreen)
        {
            case eScreen.CHARACTERSELECT:
                mainMenuAnimator.CrossFade("Main_Menu_ChangeToCharacterSelection", 0.1f);
                break;

            case eScreen.SONGCHOICE:
                mainMenuAnimator.CrossFade("Main_Menu_ChangeToSongSelection", 0.1f);
                break;
        }
    }

    public void DisableAllScreens()
    { 
        foreach(MenuScreen s in Screens)
        {
            s.gameObject.SetActive(false);
        }
    }

    public void OpenSongChoice()
    {
        OpenScreen(eScreen.SONGCHOICE);
    }

    public void OpenMainMenu()
    {
        OpenScreen(eScreen.MAIN);
    }

    public void OpenCharacterSelect()
    {
        OpenScreen(eScreen.CHARACTERSELECT);
    }
}
