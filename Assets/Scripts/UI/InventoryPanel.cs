using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public Item item;
    public void OnUse()
    {
        if(item!=null)
        {
            item.Use();
        }
        this.gameObject.SetActive(false);
    }
    public void OnDontUse()
    {
        this.gameObject.SetActive(false);
    }
}
