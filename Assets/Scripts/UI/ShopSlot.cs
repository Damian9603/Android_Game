using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject panel;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void ItemDescription()
    {
        if (item != null)
        {
            panel.SetActive(true);
            panel.GetComponentInChildren<Text>().text = item.GetDescription();
            panel.GetComponentsInChildren<Image>()[1].sprite = item.icon;
            Button[] buttons = panel.GetComponentsInChildren<Button>();
            if (item.tag == "equipment")
            {
                buttons[0].GetComponentInChildren<Text>().text = "Buy";
                buttons[1].GetComponentInChildren<Text>().text = "Don't Buy";
            }
            panel.GetComponent<InventoryPanel>().item = item;
        }
    }
}
