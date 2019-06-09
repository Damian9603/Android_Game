using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public int giveExp;
    public override void Die()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().AddExp(giveExp);

        base.Die();
        
        //Add ragdoll effect/death animation
        Destroy(gameObject);
    }
}
