using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    #region Singleton
    public static SpellBook instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Spell Book found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSpellChanged();
    SpellBook spellBook;
    public OnSpellChanged onSpellChangedCallback;

    public int space = 20;
    public List<Spell> spells = new List<Spell>();

    private void Start()
    {
        spellBook = SpellBook.instance;
    }

    public bool Add(Spell spell)
    {
        if (spells.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        spells.Add(spell);
    
        if (onSpellChangedCallback != null)
        {
            onSpellChangedCallback.Invoke();
        }
        return true;
    }
}    
