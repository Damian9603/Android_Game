using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform spellsParent;
    public Transform shopParent;
    public GameObject inventoryUI;
    public GameObject inventoryButton;
    public GameObject attackButton;
    public GameObject[] spellButton;
    public GameObject[] infoButton;
    public GameObject equipmentUI;
    public GameObject HealthPot;
    public GameObject ManaPot;
    public GameObject HealthBar;
    public GameObject ManaBar;
    public GameObject ExpBar;
    public GameObject StatsUI;
    public GameObject TreeUI;
    public GameObject SpellBookUI;
    public GameObject ShopUI;
    public Sprite defaultHealPot;
    public Sprite defaultManaPot;
    public Sprite defaultAttackSprite;
    public Text gold;
    public Text[] invPanel;
    Inventory inventory;
    SpellBook spellList;
    EquipmentManager equipment;
    Shop shop;
    InventorySlot[] slots;
    SpellSlot[] spellSlots;
    EqtSlot[] eqSlots;
    ShopSlot[] shopSlot;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        spellList = SpellBook.instance;
        spellList.onSpellChangedCallback += UpdateSUI;
        spellSlots = spellsParent.GetComponentsInChildren<SpellSlot>();
        equipment = EquipmentManager.instance;
        equipment.onEquipmentChanged += UpdateAttackIcon;
        shop = Shop.instance;
        shop.onItemChangedCallback += UpdateTUI;
        shopSlot = shopParent.GetComponentsInChildren<ShopSlot>();
    }

    void UpdateAttackIcon(Equipment newItem, Equipment oldItem)
    {
        if(newItem==null)
        {
            if(oldItem!=null)
            {
                if((int)oldItem.equipSlot==3)
                {
                    attackButton.GetComponentsInChildren<Image>()[2].sprite = defaultAttackSprite;
                }
            }
        }
        else
        {
            if((int)newItem.equipSlot==3)
            {
                attackButton.GetComponentsInChildren<Image>()[2].sprite = newItem.icon;
            }
        }
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
    void UpdateSUI()
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            if (i < spellList.spells.Count)
            {
                spellSlots[i].AddSpell(spellList.spells[i]);
            }
        }
    }
    void UpdateTUI()
    {
        for (int i = 0; i < shopSlot.Length; i++)
        {
            if (i < shop.items.Count)
            {
                shopSlot[i].AddItem(shop.items[i]);
            }
            else
            {
                shopSlot[i].ClearSlot();
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
        for(int i=0;i<4;i++) {
            spellButton[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++) {
            infoButton[i].SetActive(true);
        }
        inventoryUI.SetActive(true);
        equipmentUI.SetActive(true);
        attackButton.SetActive(false);
        HealthPot.SetActive(false);
        ManaPot.SetActive(false);
        HealthBar.SetActive(false);
        ManaBar.SetActive(false);
        ExpBar.SetActive(false);
        StatsUI.SetActive(false);
        TreeUI.SetActive(false);
        SpellBookUI.SetActive(false);
        inventory.wear = true;
        gold.text = PlayerManager.instance.player.GetComponent<PlayerStats>().gold.ToString();
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }
    void Hide()
    {
        for (int i = 0; i < 4; i++) 
            spellButton[i].SetActive(true);
        for (int i = 0; i < 4; i++) 
            infoButton[i].SetActive(false);

        inventoryUI.SetActive(false);
        attackButton.SetActive(true);
        equipmentUI.SetActive(false);
        StatsUI.SetActive(false);
        HealthPot.SetActive(true);
        ManaPot.SetActive(true);
        HealthBar.SetActive(true);
        ManaBar.SetActive(true);
        ExpBar.SetActive(true);
        TreeUI.SetActive(false);
        SpellBookUI.SetActive(false);
        ShopUI.SetActive(false);
        inventory.wear = true;
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }
    public void ShowStats()
    {
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        StatsUI.SetActive(true);
        TreeUI.SetActive(false);
        SpellBookUI.SetActive(false);
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }

    public void ShowTree()
    {
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        StatsUI.SetActive(false);
        TreeUI.SetActive(true);
        SpellBookUI.SetActive(false);
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }
    public void ShowBook()
    {
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        StatsUI.SetActive(false);
        TreeUI.SetActive(false);
        SpellBookUI.SetActive(true);
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }

    public void ShowShop()
    {
        for (int i = 0; i < 4; i++)
            spellButton[i].SetActive(false);
        for (int i = 0; i < 4; i++)
            infoButton[i].SetActive(true);
        inventoryUI.SetActive(true);
        ShopUI.SetActive(true);
        attackButton.SetActive(false);
        HealthPot.SetActive(false);
        ManaPot.SetActive(false);
        HealthBar.SetActive(false);
        ManaBar.SetActive(false);
        ExpBar.SetActive(false);
        inventory.wear = false;
        gold.text = PlayerManager.instance.player.GetComponent<PlayerStats>().gold.ToString();
        PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateUI();
    }

    public void UseHealPot()
    {
        for (int i=0;i < inventory.items.Count; i++)
        {
            
            if(inventory.items[i].tag == "heal")
            {
                inventory.items[i].Use();
                break;
            }
        }
        bool potFlag = false;
        for(int i=0; i< inventory.items.Count; i++)
        {
            if(inventory.items[i].tag == "heal")
            {
                potFlag = true;
                break;
            }
        }
        if(potFlag==false)
        {
            HealthPot.GetComponent<Image>().sprite = defaultHealPot;
        }
    }

    public void UseManaPot()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].tag == "mana")
            {
                inventory.items[i].Use();
            }
        }
        bool potFlag = false;
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].tag == "mana")
            {
                potFlag = true;
            }
        }
        if (potFlag == false)
        {
            ManaPot.GetComponent<Image>().sprite = defaultManaPot;
        }
    }
}
