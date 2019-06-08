using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public Equipment item;
    public EqtSlot[] eqtSlots;
    public void OnDeequipment()
    {
        EquipmentManager.instance.Unequip((int)item.equipSlot);
        this.gameObject.SetActive(false);
        eqtSlots[(int)item.equipSlot].ClearSlot();
    }
}
