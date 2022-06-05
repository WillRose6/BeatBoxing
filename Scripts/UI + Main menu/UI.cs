using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField]
    private TextMeshProUGUI[] RhythmScores;
    [SerializeField]
    private Slider[] ComboSliders;
    [SerializeField]
    private Slider[] HealthBars;
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private TMPro.TextMeshProUGUI[] endGameScores;
    [SerializeField]
    private TMPro.TextMeshProUGUI endGameWinnerText;

    public Animator Anim { get => anim; set => anim = value; }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("More than one UI class in scene :(");
            return;
        }

        instance = this;
    }

    public void SetRhythmScore(int playerID, int newScore)
    {
        RhythmScores[playerID].text = newScore.ToString();
    }

    public void SetCombo(int playerID, int score)
    {
        ComboSliders[playerID].value = score;
    }

    public void SetHealth(int playerID, int score)
    {
        HealthBars[playerID].value = score;
    }

    public void SlideIn()
    {
        anim.SetTrigger("SlideIn");
    }

    public void SetEndGameTexts(int player1Score, int player2Score, int winner)
    {
        endGameScores[0].text = player1Score.ToString();
        endGameScores[1].text = player2Score.ToString();

        endGameWinnerText.text = "Player " + winner + " wins!";
    }
}
