using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }
    public int avaiblePoints;
    public int skillPoints;

    public int gold;

    public Spell[] spellList;
    private int whichSlot;
    public void whichSpell(int i)
    {
        whichSlot = i;
    }
    public void Spelllist(SpellSlot slot)
    {
        spellList[whichSlot] = slot.spell;
    }

    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if(newItem!=null)
        {
            
            minDamage.AddModifier(newItem.minDamageModifier);
            maxDamage.AddModifier(newItem.maxDamageModifier);
            magicDamage.AddModifier(newItem.magicDamageModifier);
            armour.AddModifier(newItem.armorModifier);
            magicResist.AddModifier(newItem.magicResistModifier);

            strenght.AddModifier(newItem.strenghtModifier);
            agility.AddModifier(newItem.agilityModifier);
            condition.AddModifier(newItem.constitutionModifier);
            intelligence.AddModifier(newItem.intelligenceModifier);
            wisdom.AddModifier(newItem.wisdomModifier);
            charisma.AddModifier(newItem.charismaModifier);

            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;

            UpdateUI();
        }
        if(oldItem!=null)
        {

            minDamage.RemoveModifier(oldItem.minDamageModifier);
            maxDamage.RemoveModifier(oldItem.maxDamageModifier);
            magicDamage.RemoveModifier(oldItem.magicDamageModifier);
            armour.RemoveModifier(oldItem.armorModifier);
            magicResist.RemoveModifier(oldItem.magicResistModifier);

            strenght.RemoveModifier(oldItem.strenghtModifier);
            agility.RemoveModifier(oldItem.agilityModifier);
            condition.RemoveModifier(oldItem.constitutionModifier);
            intelligence.RemoveModifier(oldItem.intelligenceModifier);
            wisdom.RemoveModifier(oldItem.wisdomModifier);
            charisma.RemoveModifier(oldItem.charismaModifier);

            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;

            UpdateUI();
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();

    }

    //UI-stats
    public void Warrior(Button button)
    {
        if (skillPoints > 0)
        {
            strenght.baseValue += 3;
            condition.baseValue += 1;
            intelligence.baseValue -= 1;
            agility.baseValue -= 1;
            skillPoints -= 1;
            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;
            button.interactable = false;
            UpdateUI();
        }
        else
        {
            button.interactable = true;
        }
    }
    public void Archer(Button button)
    {
        if (skillPoints > 0)
        {
            agility.baseValue += 3;
            strenght.baseValue += 1;
            intelligence.baseValue -= 1;
            condition.baseValue -= 1;
            skillPoints -= 1;
            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;
            button.interactable = false;
            UpdateUI();
        }
        else
        {
            button.interactable = true;
        }
    }
    public void Mage(Button button)
    {
        if (skillPoints > 0)
        {
            intelligence.baseValue += 2;
            wisdom.baseValue += 1;
            strenght.baseValue -= 1;
            agility.baseValue -= 1;
            skillPoints -= 1;
            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;
            button.interactable = false;
            UpdateUI();
        }
        else
        {
            button.interactable = true;
        }
    }
    private Button lastButton;
    public void Last(Button button)
    {
        lastButton = button;
    }

    public void checkIfPossible(Button button)
    {
        if (!lastButton.interactable)
            button.interactable = true;
        else
            button.interactable = false;
    }
    private Spell newSpell;
    public Text SpellInfo;
    public void Spellnew(Spell spelly)
    {
        newSpell = spelly;
    }

    public void AddToSpellBook(Button button)
    {
        int req = 0;
        if (newSpell.required == "strenght")
            req = stats[0];
        if (newSpell.required == "agility")
            req = stats[1];
        if (newSpell.required == "condition")
            req = stats[2];
        if (newSpell.required == "intelligence")
            req = stats[3];
        if (newSpell.required == "wisdom")
            req = stats[4];
        if (newSpell.required == "charisma")
            req = stats[5];
        if (skillPoints > 0 && req >= newSpell.value)
        {
            strenght.baseValue += newSpell.gives[0];
            agility.baseValue += newSpell.gives[1];
            condition.baseValue += newSpell.gives[2];
            intelligence.baseValue += newSpell.gives[3];
            wisdom.baseValue += newSpell.gives[4];
            charisma.baseValue += newSpell.gives[5];
            stats[0] = strenght.baseValue;
            stats[1] = agility.baseValue;
            stats[2] = condition.baseValue;
            stats[3] = intelligence.baseValue;
            stats[4] = wisdom.baseValue;
            stats[5] = charisma.baseValue;
            SpellBook.instance.Add(newSpell);
            button.interactable = false;
            skillPoints -= 1;
            UpdateUI();
        }
        else
        {
            button.interactable = true;
        }
    }

    public Text lvlText;
    public Text pointsText;
    public Text strText;
    public Text dexText;
    public Text conText;
    public Text intText;
    public Text wisText;
    public Text chaText;

    public Text currHP;
    public Text currMP;
    public Text currExp;
    public Text dmgP;
    public Text[] spellDmg;
    public Text pArmor;
    public Text mArmor;
    public Text skillPoint;
    public Text goldT;

    private int maxpoints = 10;
    protected int[] stats = { 8, 8, 8, 8, 8, 8 };

    public void UpdateUI()
    {
        if (lvlText != null)
        {
            lvlText.text = level.ToString();
            pointsText.text = avaiblePoints.ToString();

            strText.text = strenght.baseValue.ToString();
            dexText.text = agility.baseValue.ToString();
            conText.text = condition.baseValue.ToString();
            intText.text = intelligence.baseValue.ToString();
            wisText.text = wisdom.baseValue.ToString();
            chaText.text = charisma.baseValue.ToString();

            currHP.text = currentHealth.ToString() + "/" + maxHealth.ToString();
            currMP.text = currentMana.ToString() + "/" + maxMana.ToString();
            currExp.text = currentExp + "/" + nextLevelExp.ToString();

            dmgP.text = minDamage.baseValue.ToString() + "-" + maxDamage.baseValue.ToString();

            pArmor.text = armour.baseValue.ToString();
            mArmor.text = magicResist.baseValue.ToString();

            skillPoint.text = skillPoints.ToString();
            goldT.text = gold.ToString();
        }
        maxHealth = startHP + condition.GetValue() * 10 + level * 5;
        maxMana = startMana + wisdom.GetValue() * 10;
        Change(maxHealth, currentHealth, 'h');
        Change(maxMana, currentMana, 'm');
    }

    public void SetStr(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            strenght.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && strenght.baseValue > stats[0])
        {
            strenght.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }
    public void SetDex(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            agility.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && agility.baseValue > stats[1])
        {
            agility.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }
    public void SetCon(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            condition.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && condition.baseValue > stats[2])
        {
            condition.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }
    public void SetInt(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            intelligence.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && intelligence.baseValue > stats[3])
        {
            intelligence.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }
    public void SetWis(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            wisdom.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && wisdom.baseValue > stats[4])
        {
            wisdom.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }
    public void SetCha(int value)
    {
        if (value > 0 && avaiblePoints > 0)
        {
            charisma.baseValue += value;
            avaiblePoints -= 1;
            UpdateUI();
        }
        else if (value < 0 && charisma.baseValue > stats[5])
        {
            charisma.baseValue += value;
            avaiblePoints += 1;
            UpdateUI();
        }
    }

    public void SaveStats()
    {
        stats[0] = strenght.baseValue;
        stats[1] = agility.baseValue;
        stats[2] = condition.baseValue;
        stats[3] = intelligence.baseValue;
        stats[4] = wisdom.baseValue;
        stats[5] = charisma.baseValue;
        maxpoints = avaiblePoints;
        UpdateUI();
    }
    public void ResetStats()
    {
        strenght.baseValue = stats[0];
        agility.baseValue = stats[1];
        condition.baseValue = stats[2];
        intelligence.baseValue = stats[3];
        wisdom.baseValue = stats[4];
        charisma.baseValue = stats[5];
        avaiblePoints = maxpoints;
        UpdateUI();
    }

    public void AddHP(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Change(maxHealth, currentHealth, 'h');
        UpdateUI();
    }

    public void AddMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        Change(maxMana, currentMana, 'm');
        UpdateUI();
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        while (currentExp > nextLevelExp)
        {
            avaiblePoints += 3 + stats[3] / (15 * level / 4);
            level++;
            currentExp -= nextLevelExp;
            nextLevelExp *= 2;
            skillPoints++;
        }
        Change(nextLevelExp, currentExp, 'e');
        UpdateUI();
    }
}
