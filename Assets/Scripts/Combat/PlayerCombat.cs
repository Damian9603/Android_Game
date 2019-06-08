using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : CharacterCombat
{
    public CharacterStats playerStats;
    public PlayerController controller;
    new public event System.Action OnAttack;
    float[] cooldowns;
    protected override void Start()
    {
        base.Start();
        cooldowns = new float[4];
    }
    protected override void Update()
    {
        base.Update();
        for(int i= 0; i<cooldowns.Length;i++)
        {
            if(cooldowns[i]<=0)
            {
                cooldowns[i] = 0;
            }
            else
            {
                cooldowns[i] -= Time.deltaTime;
            }
        }
    }
    public override void Attack(CharacterStats targetStats)
    {
        if (controller != null)
        {
            if (controller.focus == null)
            {
                return;
            }
        }
        base.Attack(targetStats);
        if (OnAttack != null)
        {
            OnAttack();
        }

    }
    public void PlayerAttack()
    {
        Interactable focus = controller.focus;
        if (focus != null && focus.tag == "Enemy")
        {
            float distance = Vector3.Distance(focus.transform.position, PlayerManager.instance.player.transform.position);
            if (distance > 3)
            {
                Debug.Log("Out of range");
            }
            else
            {
                CharacterStats focusStats = focus.GetComponent<CharacterStats>();
                Attack(focusStats);
            }
        }
        else
        {
            Debug.Log("Noone to attack");
        }
    }

    public void UseSpell(Button button)
    {
        Spell spell = null;
        int number = -1;
        PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();
        bool used = false;
        if (button.name == "SpellButton_1")
        {
            number = 0;
            spell = player.spellList[0];
            if(cooldowns[0]>0)
            {
                Debug.Log("Cooldown 0");
                
                return;
            }
        }
        if (button.name == "SpellButton_2")
        {
            number = 1;
            spell = player.spellList[1];
            if (cooldowns[1] > 0)
            {
                Debug.Log("Cooldown 1");

                return;
            }
        }
        if (button.name == "SpellButton_3")
        {
            number = 2;
            spell = player.spellList[2];
            if (cooldowns[2] > 0)
            {
                Debug.Log("Cooldown 2");
                return;
            }
        }
        if (button.name == "SpellButton_4")
        {
            number = 3;
            spell = player.spellList[3];
            if (cooldowns[3] > 0)
            {
                Debug.Log("Cooldown 3");
                return;
            }
        }
        if (spell.manaCost > player.currentMana)
            return;
        int req = 0;
        if (spell.required == "strength")
            req = player.strenght.GetValue();
        if (spell.required == "agility")
            req = player.agility.GetValue();
        if (spell.required == "condition")
            req = player.condition.GetValue();
        if (spell.required == "intelligence")
            req = player.intelligence.GetValue();
        if (spell.required == "wisdom")
            req = player.wisdom.GetValue();
        if (spell.required == "charisma")
            req = player.charisma.GetValue();
        if (spell.heal != 0)
        {
            int healAmount = spell.heal + (int)(spell.scale * player.wisdom.GetValue());
            player.AddHP(healAmount);
            player.AddMana(spell.manaCost * -1);
            used = true;
        }
        Interactable focus = controller.focus;
        if (spell.pdamage != 0)
        {
            if (focus != null && focus.tag == "Enemy")
            {
                float distance = Vector3.Distance(focus.transform.position, PlayerManager.instance.player.transform.position);
                if (!(distance > spell.range))
                {
                    CharacterStats focusStats = focus.GetComponent<CharacterStats>();
                    focusStats.TakeDamage ((int)(spell.pdamage + spell.scale * req - focusStats.armour.GetValue()));
                    player.AddMana(spell.manaCost * -1);
                    used = true;
                }
            }
        }
        if (spell.mdamage != 0)
        {
            if (focus != null && focus.tag == "Enemy")
            {
                float distance = Vector3.Distance(focus.transform.position, PlayerManager.instance.player.transform.position);
                if (!(distance > spell.range))
                {
                    CharacterStats focusStats = focus.GetComponent<CharacterStats>();
                    focusStats.TakeDamage((int)(spell.mdamage + spell.scale * req - focusStats.magicResist.GetValue()));
                    player.AddMana(spell.manaCost * -1);
                    used = true;
                }
            }
        }
        if(used)
        {
            cooldowns[number] = spell.cooldown;
            //animation for cooldown
            button.interactable = false;
            StartCoroutine(EndCooldown(button, spell.cooldown));
        }
    }

    IEnumerator EndCooldown(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }
}
