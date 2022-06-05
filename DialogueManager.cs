using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    public static DialogueManager instance;

    [Header("UI")]
    public Image player1;
    public Image player2;
    public Image counter;
    public Text text;
    public Text roundCounter;
    public GameObject dialoguePanel;
    public GameObject countdownPanel;

    [Header("Sprites")]
    public Sprite three;
    public Sprite two;
    public Sprite one;
    public Sprite fight;

    [Header("Variables")]
    public int dialogueTime;

    private bool countdownStarted;
    private float countdownStartTime;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        countdownStarted = false;
    }

    public void HideScreen()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void DisplayScreen()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
    }


    void DisplayText()
    {
        //to add more add another switch case, then increase the second number of the random range by however many you added  - Zoey xxx

        int saying = Random.Range(1, 13);

        switch (saying)
        {
            case 1:
                text.text = "You call that music, punk?";
                break;
            case 2:
                text.text = "I'm gonna sneeze in your open mouth";
                break;
            case 3:
                text.text = "could you turn it down a tad it's real loud";
                break;
            case 4:
                text.text = "now watch me wipe the floor with you";
                break;
            case 5:
                text.text = "*ominous silence*...oh you finished now?";
                break;
            case 6:
                text.text = "What a sad excuse for an artist you are";
                break;
            case 7:
                text.text = "I've heard better BTEC music";
                break;
            case 8:
                text.text = "Pft you made this too easy to beat you";
                break;
            case 9:
                text.text = "I'm gonna punch the highlights out of your hair!";
                break;
            case 10:
                text.text = "Your ass is grass and im gonna mow it";
                break;

            case 11:
                text.text = "Hope you like second place";
                break;
            case 12:
                text.text = "Was that meant to impress me?";
                break;

            default:
                text.text = "Grrrr";
                break;
        }
    }

    //Needs to call this with the int as the round number
    public void Display(int round)
    {
        countdownStartTime = Time.time;
        countdownStarted = true;

        dialoguePanel.SetActive(true);
        countdownPanel.SetActive(false);

        DisplayText();

        //Update round number
        roundCounter.text = "Round " + round;
        
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B)){ Display(2); }

        if (countdownStarted)
        {
            if ((Time.time <= countdownStartTime + dialogueTime + 1) && (Time.time > countdownStartTime + dialogueTime))
            {
                //display 3
                dialoguePanel.SetActive(false);
                countdownPanel.SetActive(true);
                counter.sprite = three;
            }
            else if (Time.time <= countdownStartTime + dialogueTime + 2)
            {
                //display 2
                counter.sprite = two;
            }
            else if (Time.time <= countdownStartTime + dialogueTime + 3)
            {
                //display 1
                counter.sprite = one;
            }
            else if (Time.time <= countdownStartTime + dialogueTime + 4)
            {
                //display 'fight'
                counter.sprite = fight;
            }
            else if (Time.time > countdownStartTime + dialogueTime + 4)
            {
                //hide all
                //gameObject.GetComponent<Canvas>().enabled = false;
            }
        }

    }

   
}
