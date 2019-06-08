using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int minDamageModifier;
    public int maxDamageModifier;
    public int armorModifier;
    public int magicDamageModifier;
    public int magicResistModifier;
    public int strenghtModifier;
    public int agilityModifier;
    public int constitutionModifier;
    public int intelligenceModifier;
    public int wisdomModifier;
    public int charismaModifier;

    //0 melee, -1 not a weapon
    public int range = -1;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFormInventory();
    }
    public override string GetDescription()
    {
        string desc = name;
        desc += "\n" + description;
        desc += "\n" + equipSlot;
        if (minDamageModifier>0)
        {
            desc += "\nDamage: " + minDamageModifier + "-" + maxDamageModifier;
        }
        if(armorModifier>0)
        {
            desc += "\nPhysical Armour: " + armorModifier;
        }
        if(magicResistModifier>0)
        {
            desc += "\nMagic Armour: " + magicResistModifier;
        }
        if(strenghtModifier>0)
        {
            desc += "\nStrenght: +" + strenghtModifier;
        }
        if(agilityModifier>0)
        {
            desc += "\nAgility: +" + agilityModifier;
        }
        if (constitutionModifier > 0)
        {
            desc += "\nConstitution: +" + constitutionModifier;
        }
        if(intelligenceModifier>0)
        {
            desc += "\nInteligence: +" + intelligenceModifier;
        }
        if (wisdomModifier > 0)
        {
            desc += "\nWisdom: +" + wisdomModifier;
        }
        if(charismaModifier>0)
        {
            desc += "\nCharisma: +" + charismaModifier;
        }
        return desc;
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
public enum EquipmentMeshRegion { Legs, Arms, Torso}; //Corresponds to body blendshapes
