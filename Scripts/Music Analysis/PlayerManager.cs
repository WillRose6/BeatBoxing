using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField]
    private Player[] players;

    public Player[] Players { get => players; set => players = value; }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("PlayerManager already exists!");
            return;
        }
        instance = this;
    }

    public Player getPlayerFromID(int ID)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].PlayerID == ID)
            {
                return players[i];
            }
        }

        return null;
    }
}
