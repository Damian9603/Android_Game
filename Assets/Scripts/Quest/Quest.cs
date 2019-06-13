using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item ItemReward { get; set; }
    public int GoldReward { get; set; }
    public bool Completed { get; set; }

    public void Check()
    {
        Completed = Goals.All(g => g.Completed);
    }

    public void GiveReward()
    {
        if(ItemReward != null)
        {
            Inventory.instance.Add(ItemReward);
        }
        PlayerManager.instance.player.GetComponent<PlayerStats>().AddExp(ExperienceReward);
        PlayerManager.instance.player.GetComponent<PlayerStats>().gold += GoldReward;
    }
}
