using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public static Character[] charsToLoad;
    public Transform[] spawnPoints;
    private void Start()
    {
        LoadInCharacters();
    }

    private void LoadInCharacters()
    {
        for(int i = 0; i < charsToLoad.Length; i++)
        {
            Transform pos = spawnPoints[i];
            GameObject g = Instantiate(charsToLoad[i].inGamePrefab, pos.position, pos.rotation);
            RhythmPlayer p = PlayerManager.instance.Players[i].GetComponent<RhythmPlayer>();
            p.characterAnimator = g.GetComponent<PlayerAnimator>();
        }
    }
}
