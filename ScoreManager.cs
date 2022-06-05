using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void Start()
    {
        foreach(Player p in PlayerManager.instance.Players)
        {
            GameManager.instance.Ui.SetRhythmScore(p.PlayerID, p.GetScore());
        }
    }

    public void AddScoreToPlayer(int id, int amount)
    {
        Player p = PlayerManager.instance.Players[id];
        p.ChangeScore(amount);
        GameManager.instance.Ui.SetRhythmScore(id, p.GetScore());
    }
}
