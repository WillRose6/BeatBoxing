using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmPlayer : Player
{
    [SerializeField]
    private int combo;
    [SerializeField]
    private UI ui;
    private ParticleSystem ring;
    private ParticleSystem smoke;
    private ParticleSystem heal;
    private ParticleSystem c_c_c_combo;
    private float startTime1;
    private float startTime2;
    private float startTime3;
    private float startTime4;
    private GameObject blocker;

    [SerializeField]
    private GameObject comboEffect;

    [SerializeField]
    private ParticleManager particleManager;

    private bool blocking;
    private bool healing;
    //private bool c_c_c_comboing;

    public int Combo { get => combo; set => combo = value; }

    public int costOne;
    public int costTwo;
    public int costThree;

    public bool isAI;
    private bool canSpawnComboThing;

    public PlayerAnimator characterAnimator;

    //Power-up 1: Score bonus
    //Power-up 2: More notes for enemy
    //Power-up 3: Signature move
    protected override void Start()
    {
        base.Start();
        blocker = GameObject.FindGameObjectWithTag("Blocker" + ((PlayerID + 1) % 2));
        blocker.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, false);

        ring = particleManager.Ring;
        smoke = particleManager.Smoke;
        heal = particleManager.Shield;

        //ring.gameObject.SetActive(false);
        //smoke.gameObject.SetActive(false);
        //heal.gameObject.SetActive(false);

        ring.Stop();
        smoke.Stop();
        heal.Stop();
        //c_c_c_combo.Stop();
    }

    void Activate()
    {
        if (combo >= costThree) { powerThree(); }
        else if (combo >= costTwo) { powerTwo(); }
        else if (combo >= costOne){ powerOne(); }
        else { return; }
    }

    void powerOne()
    {
        //activate score multiplier for 5 seconds
        Multiplier = true;
        startTime1 = Time.time;
        combo -= costOne;
        //activate ring
        //ring.gameObject.SetActive(true);
        ring.Play();
    }

    void powerTwo()
    {
        //Blind enemy player
        blocker.SetActive(true);
        blocker.GetComponent<Image>().CrossFadeAlpha(0.99f, 1.0f, false);
        blocking = true;
        startTime2 = Time.time;
        combo -= costTwo;
        //activate smoke
        
        //smoke.gameObject.SetActive(true);
        smoke.Play();
    }

    void powerThree()
    {
        //do cool effects
        //take away a bunch of enemy health

        float healthDifference = StartHealth - Health;
        Health += healthDifference * 0.25f;
        healing = true;
        startTime3 = Time.time;
        combo -= costThree;
        //activate heal shield
        //heal.gameObject.SetActive(true);
        heal.Play();
    }

    void displayComboThing()
    {
        //c_c_c_comboing = true;
        //startTime4 = Time.time;
        GameObject g = Instantiate(comboEffect, characterAnimator.gameObject.transform.position + new Vector3(0, 5f, 0), Quaternion.identity);
        Destroy(g, 3f);
        //c_c_c_combo.Play();
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        if (amount < 0)
        {
            characterAnimator.TakeDamage();
        }
    }

    private void Update()
    {
        if (Multiplier)
        {
            if (Time.time >= startTime1 + 5.0f)
            {
                Multiplier = false;
                ring.Stop();
                //ring.gameObject.SetActive(false);
            }
        }
        if (blocking)
        {
            if (Time.time >= startTime2 + 5.0f)
            {
                blocking = false;
                blocker.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
                smoke.Stop();
                //smoke.gameObject.SetActive(false);
            }
        }
        if (healing)
        {
            if (Time.time >= startTime3 + 1.5f)
            {
                healing = false;
                heal.Stop();
                //heal.gameObject.SetActive(false);
            }
        }


        if (combo > costTwo)
        {
            if (canSpawnComboThing)
            {
                canSpawnComboThing = false;
                //display combo particle effect
                displayComboThing();
            }
        }
        else
        {
            canSpawnComboThing = true;
        }

        if (Input.GetButtonDown(controlScheme.PowerupButton + (PlayerID + 1)))
        {
            Activate();
        }

        //update the UI class
        ui.SetCombo(PlayerID, combo);
        ui.SetHealth(PlayerID, Mathf.RoundToInt(Health));

        //if (Input.GetKey(KeyCode.Z))
        //{
        //    Debug.Log("power 1");
        //    powerOne();
        //}

        //if (Input.GetKey(KeyCode.X))
        //{
        //    powerTwo();
        //}

        //if (Input.GetKey(KeyCode.C))
        //{
        //    powerThree();
        //}
    }

    public override void ChangeStance(Stance newStance)
    {
        base.ChangeStance(newStance);

        if (newStance == Stance.ATTACKING)
        {
            characterAnimator.Strum();
        }
        else if(newStance == Stance.DEFENDING)
        {
            characterAnimator.Idle();
        }
    }
}
