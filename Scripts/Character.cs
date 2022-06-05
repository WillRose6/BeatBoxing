using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character", order = 1)]
public class Character : ScriptableObject
{
    public GameObject mainMenuPrefab;
    public GameObject inGamePrefab;
    public string characterName;
}
