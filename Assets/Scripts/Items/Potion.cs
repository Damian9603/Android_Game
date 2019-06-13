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
        stats.AddHP(healthAmount + (PlayerManager.instance.player.GetComponent<PlayerStats>().maxHealth - PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth) * healthAmount / 100);
        stats.AddMana(manaAmount + (PlayerManager.instance.player.GetComponent<PlayerStats>().maxMana - PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana) * manaAmount / 100);
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
