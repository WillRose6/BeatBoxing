using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Player player;
    public Animator[] anims;

    private void Start()
    {
        player = PlayerManager.instance.Players[0];
    }

    public void TakeDamage()
    {
        foreach (Animator a in anims)
        {
            a.SetTrigger("TakeDamage");
        }
    }

    public void Strum()
    {
        foreach (Animator a in anims)
        {
            a.SetBool("Attacking", true);
        }
    }

    public void Idle()
    {
        foreach (Animator a in anims)
        {
            a.SetBool("Attacking", false);
        }
    }
}
