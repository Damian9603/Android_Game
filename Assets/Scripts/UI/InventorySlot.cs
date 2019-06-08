using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
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
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void ItemDescription()
    {
        if(item!=null)
        {
            panel.SetActive(true);
            panel.GetComponentInChildren<Text>().text = item.GetDescription();
            panel.GetComponentsInChildren<Image>()[1].sprite = item.icon;
            Button[] buttons = panel.GetComponentsInChildren<Button>();
            if(item.tag== "equipment")
            {
                buttons[0].GetComponentInChildren<Text>().text = "Wear";
                buttons[1].GetComponentInChildren<Text>().text = "Don't wear";
            }
            else
            {
                buttons[0].GetComponentInChildren<Text>().text = "Use";
                buttons[1].GetComponentInChildren<Text>().text = "Don't use";
            }
            panel.GetComponent<InventoryPanel>().item = item;
        }
    }
}
