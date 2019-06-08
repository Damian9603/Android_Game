using UnityEngine;
using UnityEngine.UI;

public class EqtSlot : MonoBehaviour
{
    public Image icon;
    Item item;
    private void Start()
    {
        item = null; 
    }
    public void AddItem(Equipment newItem)
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
    }
}
