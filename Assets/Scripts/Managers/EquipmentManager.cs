using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] defaultItems;
    public Equipment[] currentEquipment;
    Inventory inventory;
    public EqtSlot[] eqtSlots = new EqtSlot[6];

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {        
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null; 
            Unequip(slotIndex);
        if (currentEquipment[slotIndex]!=null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged!=null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        SetEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        eqtSlots[slotIndex].AddItem(currentEquipment[slotIndex]);

    }

    public void Unequip (int slotIndex)
    {
        if(currentEquipment[slotIndex]!=null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            //return oldItem;

            
        }
        //return null;
    }

    public void UnequipAll()
    {
        for(int i=0; i<currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        /*foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }*/
    }


}
