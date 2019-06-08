using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().AddExp(10);
        base.Die();
        
        //Add ragdoll effect/death animation
        Destroy(gameObject);
    }
}
