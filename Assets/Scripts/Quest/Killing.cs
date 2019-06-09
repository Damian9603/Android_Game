using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killing : Goal
{
    PlayerController contr;
    public int EnemyID { get; set; }

    public Killing(int enemyID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.EnemyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        Interactable enemy = contr.focus;
    }

    void Died(Enemy enemy)
    {
        if(enemy.ID == this.EnemyID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
