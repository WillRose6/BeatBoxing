using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvPGameMode : MonoBehaviour
{
    public Player[] players { get => PlayerManager.instance.Players; set => PlayerManager.instance.Players = value; }
    private RoundSwitcher roundSwitcher;
    [SerializeField]
    private AudioClip recordScratch;
    public AudioClip RecordScratch { get => recordScratch; set => recordScratch = value; }

    public Canvas roundScreen;

    public Canvas UIScreen;

    public Canvas endScreen;
    private int round;

    public GameObject mainCamera;
    protected virtual void Start()
    {
        roundScreen.GetComponent<Canvas>().enabled = false;
        roundSwitcher = gameObject.AddComponent<RoundSwitcher>();
        roundSwitcher.gameMode = this;
        StartCoroutine(roundSwitcher.ChangeRound());
        InvokeRepeating("CompareScores", 10f, 10f);
    }

    private void CompareScores()
    {
        Player attack = null;
        Player defend = null;
        if (round != 3)
        {
            if (players[0].playerStance == Player.Stance.ATTACKING)
            {
                attack = players[0];
                defend = players[1];
            }
            else
            {
                attack = players[1];
                defend = players[0];
            }
        }
        else
        {
            attack = players[0];
            defend = players[1];
        }

        if(attack.GetScore() > defend.GetScore())
        {
            int difference = Mathf.CeilToInt(attack.GetScore() - defend.GetScore());
            //defend.ChangeHealth(-difference);
            DealDamage(difference, defend);
        }
        else if(round == 3 && defend.GetScore() > attack.GetScore())
        {
            int difference = Mathf.CeilToInt(defend.GetScore() - attack.GetScore());
            //attack.ChangeHealth(-difference);
            DealDamage(difference, attack);
        }
    }

    private void DealDamage(int difference, Player target)
    {
        if (difference <= 1000) { target.ChangeHealth(-50); }
        else if (difference <= 2000) { target.ChangeHealth(-100); }
        else if (difference <= 5000) { target.ChangeHealth(-150); }
        else if (difference <= 10000) { target.ChangeHealth(-200); }
        else { target.ChangeHealth(-250); }
    }

    //private void checkScores()
    //{
    //    int playerOneScore = PlayerManager.instance.Players[0].GetScore();
    //    int playerTwoScore = PlayerManager.instance.Players[1].GetScore();
    //    if(playerOneScore > playerTwoScore) //one bigger than two
    //    {
    //        print("player one view");
    //        CameraController.instance.PlayerOneView();
    //    }
    //    print("playertwo view");
    //    CameraController.instance.PlayerTwoView();

    //}
}

class RoundSwitcher : MonoBehaviour
{
    public PvPGameMode gameMode;


    public IEnumerator ChangeRound()
    {
        gameMode.mainCamera.SetActive(false);
        gameMode.roundScreen.enabled = false;
        gameMode.UIScreen.enabled = false;
        gameMode.endScreen.enabled = false;
        AudioManager.instance.PlayBackgroundMusic();
        AudioManager.instance.stopCheering();
        yield return new WaitForSeconds(3);
        //AudioManager.instance.stopCheering();
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            gameMode.mainCamera.SetActive(true);
            PathFollower.instance.cancelCamera();
            AudioManager.instance.StopBackgroundMusic();
        }
        else if (!Input.GetKey(KeyCode.KeypadEnter)){
            yield return new WaitForSeconds(15f);
            gameMode.mainCamera.SetActive(true);
        }
        gameMode.mainCamera.SetActive(true);
        gameMode.UIScreen.enabled = true;
        gameMode.players[0].ChangeStance(Player.Stance.ATTACKING);
        gameMode.players[1].ChangeStance(Player.Stance.DEFENDING);
        UI.instance.SlideIn();
     
        InvokeRepeating("changeViews", 0f, 10f);
        gameMode.mainCamera.SetActive(true);
        yield return new WaitForSeconds(60); //CHANGE BACK TO 60
        CancelInvoke("changeViews");

        AudioManager.instance.startCheering();
        //1) Record scratch & stop music
        AudioManager.instance.PlayEffect(gameMode.RecordScratch);
        

        
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.StopMusic();

        //2) ROUND 2 SHIT PARTY :DD

        gameMode.players[0].ChangeStance(Player.Stance.DEFENDING);
        gameMode.players[1].ChangeStance(Player.Stance.ATTACKING);

        AudioManager.instance.LoadInSong(1);
        AudioManager.instance.stopCheering();
        gameMode.roundScreen.enabled = true;
        CameraController.instance.PlayerOneView();
        DialogueManager.instance.Display(2);
        yield return new WaitForSeconds(5);
        CameraController.instance.PlayerTwoView();
        yield return new WaitForSeconds(4);
        gameMode.roundScreen.enabled = false;
        AudioManager.instance.StartMusic();
        
        InvokeRepeating("changeViews", 0f, 10f);

        yield return new WaitForSeconds(60); //CHANGE BACK TO 60
        CancelInvoke("changeViews");


        //1) Record scratch & stop music
        AudioManager.instance.PlayEffect(gameMode.RecordScratch);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.StopMusic();

        //ROUND 3 BBY 
        gameMode.players[0].ChangeStance(Player.Stance.ATTACKING);
        gameMode.players[1].ChangeStance(Player.Stance.ATTACKING);
        AudioManager.instance.LoadInSong(2);
        gameMode.roundScreen.enabled = true;
        DialogueManager.instance.Display(3);
        yield return new WaitForSeconds(5);
        InvokeRepeating("checkScores", 0f, 10f);
        yield return new WaitForSeconds(3);
        gameMode.roundScreen.enabled = false;
        AudioManager.instance.StartMusic();
        yield return new WaitForSeconds(60);
        AudioManager.instance.PlayEffect(gameMode.RecordScratch);
        AudioManager.instance.StopMusic();
        gameMode.endScreen.enabled = true;
        print("END GAME BBY");

        int player1Score = PlayerManager.instance.Players[0].GetScore();
        int player2Score = PlayerManager.instance.Players[1].GetScore();
        int winner = 1;

        if(player2Score > player1Score)
        {
            winner = 2;
        }

        UI.instance.SetEndGameTexts(player1Score, player2Score, winner);

        //3) Countdown
        //4) Hide round change screen
        //5) Camera pans to other player and swaps attacker and defender
        //6) Plays new music
    }

    private void checkScores()
    {
        int playerOneScore = PlayerManager.instance.Players[0].GetScore();
        int playerTwoScore = PlayerManager.instance.Players[1].GetScore();
        if (playerOneScore > playerTwoScore) //one bigger than two
        {
            print("player one view");
            CameraController.instance.PlayerOneView();
        }
        if (playerOneScore < playerTwoScore) //one bigger than two
        {
            print("player TWO view");
            CameraController.instance.PlayerTwoView();
        }
       
    }

    private void changeViews()
    {
        CameraController.instance.randomAngle();
    }

    private IEnumerator playIntro()
    {
        print("started");  
        yield return new WaitForSeconds(21);
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            print("hi");
        }
        print("end");
    }
}
