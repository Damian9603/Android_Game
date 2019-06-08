using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public int healthAmount = 0;
    public int manaAmount = 0;
    public override void Use()
    {
        base.Use();
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stats.AddHP(healthAmount);
        stats.AddMana(manaAmount);
        RemoveFormInventory();
    }
    public override string GetDescription()
    {
        string desc = name;
        desc += "\n" + description;
        if(healthAmount>0)
        {
            description += "\nHeals: " + healthAmount;
        }
        if(manaAmount>0)
        {
            description += "\nAdds Mana: " + manaAmount;
        }
        return desc;
    }
}
