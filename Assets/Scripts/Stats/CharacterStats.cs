using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 100;
    public int nextLevelExp = 100;
    protected int startHP;
    protected int startMana;
    public int currentHealth { get; set; }
    public int currentMana { get; set; }
    public int currentExp { get; set; }

    public Stat minDamage;
    public Stat maxDamage;
    public Stat magicDamage;
    public Stat armour;
    public Stat magicResist;

    public Stat strenght;
    public Stat agility;
    public Stat condition;
    public Stat intelligence;
    public Stat wisdom;
    public Stat charisma;
    public int level;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action<int, int> OnManaChanged;
    public event System.Action<int, int> OnExpChanged;

    public void Change(int max, int curr, char which)
    {
        if (which == 'h')
        {
            if(OnHealthChanged != null)
                OnHealthChanged.Invoke(max, curr);
        }
        if (which == 'm')
        {
            if(OnManaChanged != null)
                OnManaChanged.Invoke(max, curr);
        }
        if (which == 'e')
        {
            if (OnExpChanged != null)
                OnExpChanged.Invoke(max, curr);
        }
    }

    private void Awake()
    {
        startHP = maxHealth;
        startMana = maxMana;
        maxHealth += condition.GetValue() * 10 + level * 5;
        maxMana +=  wisdom.GetValue() * 10;
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    private void Update()
    {
        
    }
    public void TakeDamage (int damage)
    {
        //damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);


        currentHealth -= damage;
        
        Debug.Log(transform.name + " takes " + damage + " damage.");

        Change(maxHealth, currentHealth, 'h');

        Change(maxMana, currentMana, 'm');

        Change(nextLevelExp, currentExp, 'e');

        if(currentHealth<=0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way;
        Debug.Log(transform.name + " died.");
    }

}
