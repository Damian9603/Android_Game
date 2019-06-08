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
    public SkinnedMeshRenderer targetMesh;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    Inventory inventory;
    public EqtSlot[] eqtSlots = new EqtSlot[6];

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
        
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
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public void Unequip (int slotIndex)
    {
        if(currentEquipment[slotIndex]!=null)
        {
            if(currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);

            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            //return oldItem;
            if ((int)oldItem.equipSlot != 3 && (int)oldItem.equipSlot != 4)
            {
                if((int)oldItem.equipSlot < 4)
                {
                    EquipDefaultItem((int)oldItem.equipSlot);
                }
                else
                {
                    EquipDefaultItem(3);
                }
            }
            
        }
        //return null;
    }

    public void UnequipAll()
    {
        for(int i=0; i<currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach(Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
    void EquipDefaultItem(int index)
    {
        Equip(defaultItems[index]);
    }
}
