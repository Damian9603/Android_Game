using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{
    public Item item;
    public Button healthButton;
    public Button manaButton;


    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up "+item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if(healthButton!=null)
        {
            healthButton.image.sprite = item.icon;
        }
        if(manaButton!=null)
        {
            manaButton.image.sprite = item.icon;
        }
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}
