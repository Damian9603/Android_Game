using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSkeleton : Quest
{ 
    void Start()
    {
        QuestName = "The Bone Guy";
        Description = "Kill the skeleton over the bridge";
        ExperienceReward = 200;
        GoldReward = 100;

        Goals.Add(new Killing(this, 0, "Kill skeleton", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }

}
