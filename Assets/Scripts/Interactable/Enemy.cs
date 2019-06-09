using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    public int ID;
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<PlayerCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
        else
        {
            Debug.Log("Player Combat not exists!");
        }
    }
}
