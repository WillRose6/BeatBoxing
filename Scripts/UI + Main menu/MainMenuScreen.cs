using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MenuScreen
{
    public Button quitButton;
    public Button[] modeSelectButtons;
    public int modeSelectID;

    protected override void Start()
    {
        base.Start();
    }
}
