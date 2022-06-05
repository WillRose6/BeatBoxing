using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameMode : PvPGameMode
{
    protected override void Start()
    {
        base.Start();
        SetupPlayer2AsAI();
    }

    public void SetupPlayer2AsAI()
    {
        RhythmPlayer p = PlayerManager.instance.Players[1] as RhythmPlayer;
        p.isAI = true;
    }
}
