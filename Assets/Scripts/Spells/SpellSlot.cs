using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image icon;
    public Spell spell;

    public void AddSpell(Spell newSpell)
    {
        spell = newSpell;
        icon.sprite = spell.icon;
        icon.enabled = true;
    }

    public void UseSpell(Button spellButton)
    {
        if (spell != null)
        {
            spellButton.GetComponent<Image>().sprite = spell.iconButton;
            if(spell.heal>0)
            {
                spellButton.tag = "healButton";
            }
            
        }
    }
}
