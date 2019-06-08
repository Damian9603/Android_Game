using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject inventoryButton;
    public GameObject attackButton;
    public GameObject fireballButton;
    public GameObject equipmentUI;
    public GameObject equipmentButton;
    Inventory inventory;
    EquipmentManager equipment;
    InventorySlot[] slots;
    EqtSlot[] eqSlots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }*/
    }

    void UpdateUI()
    {
       for(int i = 0; i< slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateEq()
    {
        
    }
    public void HideInventory()
    {
        Invoke("Hide", .1f);
    }

    public void ShowInventory()
    {
        inventoryUI.SetActive(true);
        attackButton.SetActive(false);
        fireballButton.SetActive(false);
        equipmentButton.SetActive(true);
        equipmentUI.SetActive(false);
    }
    public void ShowEquipment()
    {
        equipmentUI.SetActive(true);
    }
    void Hide()
    {
        inventoryUI.SetActive(false);
        attackButton.SetActive(true);
        fireballButton.SetActive(true);
        equipmentButton.SetActive(false);
        equipmentUI.SetActive(false);
    }
}
