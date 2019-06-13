using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public Item item;
    public void OnUse()
    {
        if (Inventory.instance.wear)
            item.Use();
        if (!Inventory.instance.wear)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().gold += item.value;
            PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
            Inventory.instance.Remove(item);
            Shop.instance.Add(item);
        }
        this.gameObject.SetActive(false);
    }
    public void OnDont()
    {
        this.gameObject.SetActive(false);
    }

    public void OnBuy()
    {
        if(PlayerManager.instance.player.GetComponent<PlayerStats>().gold >= item.value)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().gold -= item.value;
            PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
            Inventory.instance.Add(item);
            Shop.instance.Remove(item);
        }
        this.gameObject.SetActive(false);
    }
}
