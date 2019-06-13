using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public int ID;
    public int giveExp;
    public override void Die()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().AddExp(giveExp + giveExp * level / 10);
        CombatEvents.EnemyDied(this);
        base.Die();
        
        //Add ragdoll effect/death animation
        Destroy(gameObject);
    }
}
