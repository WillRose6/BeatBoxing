using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public MainMenu.eScreen screenType;
    public MainMenuPlayer PlayerOne { get => MainMenuManager.instance.mainMenuPlayers[0]; set => MainMenuManager.instance.mainMenuPlayers[0] = value; }
    public MainMenuPlayer PlayerTwo { get => MainMenuManager.instance.mainMenuPlayers[1]; set => MainMenuManager.instance.mainMenuPlayers[1] = value; }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }
    public virtual void ChangeScreen()
    {

    }
}
