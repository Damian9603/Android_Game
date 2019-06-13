using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    public static Dialogues Instance { get; set; }
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();

    Button continueButton;
    Text dialogueText, nameText;
    int dialogueIndex = 0;
    bool trader;
    NPC npc;

    //Quest
    bool qGiver;
    bool GotQuest { get; set; }
    bool Done { get; set; }
    List<string> bQuestDialogue = new List<string>();
    string dQuestDialogue;
    string aQuestDialogue;
    Button questButton;
    int questIndex;

    [SerializeField]
    private GameObject quests;
    private string questType;
    private Quest Quest { get; set; }

    private void Awake()
    {
        continueButton = dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("TextField").Find("TextPanel").Find("TalkText").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("NamePanel").Find("NameText").GetComponent<Text>();
        questButton = dialoguePanel.transform.Find("ButtonField").Find("Quest").GetComponent<Button>();

        continueButton.onClick.AddListener(delegate { ContinueDial(); });
        questButton.onClick.AddListener(delegate { QuestDial(); });
        dialoguePanel.SetActive(false);
        dialogueIndex = 0;
        questIndex = - 1;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialog(string[] lines, string[] bQuestLines, string dQuestLines, string aQuestLines, string name, bool trader, bool qGiver, string questName, NPC npc)
    {
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        bQuestDialogue.AddRange(bQuestLines);
        this.dQuestDialogue = dQuestLines;
        this.aQuestDialogue = aQuestLines;
        this.npcName = name;
        this.trader = trader;
        this.qGiver = qGiver;
        this.questType = questName;
        this.npc = npc;
        if (!npc.interacted)
            dialogueIndex = 0;
        else
            dialogueIndex = dialogueLines.Count - 1;
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialoguePanel.transform.Find("ButtonField").Find("Continue").Find("Text").GetComponent<Text>().text = "Continue";
            dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = false;
            dialoguePanel.transform.Find("ButtonField").Find("Quest").GetComponent<Button>().interactable = false;
        }
        else
        {
            if (trader)
                dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = true;
            if (qGiver && !Done)
                dialoguePanel.transform.Find("ButtonField").Find("Quest").GetComponent<Button>().interactable = true;
        }
        
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDial()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            if (dialogueIndex == dialogueLines.Count - 2)
            {
                dialoguePanel.transform.Find("ButtonField").Find("Continue").Find("Text").GetComponent<Text>().text = "Exit";
                if (trader)
                    dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = true;
                if (qGiver && !Done)
                    dialoguePanel.transform.Find("ButtonField").Find("Quest").GetComponent<Button>().interactable = true;
                npc.interacted = true;
            }
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            int count = Shop.instance.items.Count;
            for (int i = 0; i < count; i++)
            {
                Shop.instance.Remove(Shop.instance.items[0]);
            }
            bQuestDialogue.Clear();
            dialoguePanel.SetActive(false);
        }
    }

    public void QuestDial()
    {
        Debug.Log("1");
        if (!GotQuest)
        {
            Debug.Log("1.1");
            dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Continue";
            if (questIndex < bQuestDialogue.Count - 1)
            {
                Debug.Log("1.2");
                dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = false;
                dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = false;
                if (questIndex == bQuestDialogue.Count - 2)
                {
                    dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Exit";
                }
                questIndex++;
                dialogueText.text = bQuestDialogue[questIndex];
            }
            else
            {
                Debug.Log("1.3");
                if (trader)
                    dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = true;

                dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = true;
                dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Quest";

                int count = Shop.instance.items.Count;
                for (int i = 0; i < count; i++)
                {
                    Shop.instance.Remove(Shop.instance.items[0]);
                }
                AssignQuest();
                bQuestDialogue.Clear();
                dialoguePanel.SetActive(false);
            }
        }
        else if (!Done)
        {
            Debug.Log("2");
            CheckQuest();
            if (Done)
            {
                Debug.Log("2.1");
                questIndex++;
            }
            else if (questIndex < bQuestDialogue.Count)
            {
                Debug.Log("3");
                dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = false;
                dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = false;
                dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Exit";
                questIndex++;
                dialogueText.text = dQuestDialogue;
            }
            else
            {
                Debug.Log("4");
                if (trader)
                    dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = true;

                dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = true;
                dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Quest";

                int count = Shop.instance.items.Count;
                for (int i = 0; i < count; i++)
                {
                    Shop.instance.Remove(Shop.instance.items[0]);
                }
                bQuestDialogue.Clear();
                dialoguePanel.SetActive(false);
                questIndex--;
            }
        }
        else if (questIndex >= bQuestDialogue.Count)
        {
            Debug.Log("2.2" + questIndex.ToString());
            if (trader)
                dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = true;

            dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = true;
            dialoguePanel.transform.Find("ButtonField").Find("Quest").GetComponent<Button>().interactable = false;
            dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Quest";

            int count = Shop.instance.items.Count;
            for (int i = 0; i < count; i++)
            {
                Shop.instance.Remove(Shop.instance.items[0]);
            }
            dialoguePanel.SetActive(false);
            return;
        }
    }

    void AssignQuest()
    {
        GotQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if(Quest.Completed)
        {
            Quest.GiveReward();
            Done = true;
            dialoguePanel.transform.Find("ButtonField").Find("Continue").GetComponent<Button>().interactable = false;
            dialoguePanel.transform.Find("ButtonField").Find("Shop").GetComponent<Button>().interactable = false;
            dialoguePanel.transform.Find("ButtonField").Find("Quest").Find("Text").GetComponent<Text>().text = "Exit";
            dialogueText.text = aQuestDialogue;
        }
    }
    
    public void UpdateShop(GameObject npc)
    {
        if(!Inventory.instance.wear)
            npc.transform.Find(npcName).GetComponent<NPC>().items = Shop.instance.items.ToArray();
    }
}
