using UnityEngine;
using UnityEngine.UI;

public class EqtSlot : MonoBehaviour
{
    public Image icon;
    public GameObject panel;
    Item item;
    private void Start()
    {
        item = null;
        ClearSlot();
    }
    public void AddItem(Equipment newItem)
    {
        
        item = newItem;
        if(item.isDefaultItem==false)
        {
            icon.sprite = item.icon;
        }
        else
        {
            icon.sprite = null;
        }
        
        icon.enabled = true;
    }
    public void ItemDescription()
    {
        if (item != null)
        {
            panel.SetActive(true);
            panel.GetComponentInChildren<Text>().text = item.GetDescription();
            panel.GetComponentsInChildren<Image>()[1].sprite = item.icon;
            panel.GetComponent<EquipmentPanel>().item = (Equipment)item;
        }
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
