using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueRadio : MonoBehaviour
{

    [SerializeField] private List<string> introMessages;
    [SerializeField] private List<string> upgradeMessages;
    [SerializeField] private List<ConvoSO> conversations;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject upgradeButtons;
    [SerializeField] private GameObject talkButtons;

    [Header("Upgrade Costs")]

    private ConvoSO conversation; //Used fpr storing conversation
    private int conversationIndex = 0;

    private void Start()
    {
        MainMenu();
    }

    private void OnEnable()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        //Shows the correct buttons
        mainMenuButtons.SetActive(true);
        talkButtons.SetActive(false);

        //Picks random intro message for the radio
        text.text = introMessages[Random.Range((int)0, (int)introMessages.Count)];
    }

    public void Talk()
    {
        if (conversations.Count > 0)
        {
            //Shows the correct buttons
            mainMenuButtons.SetActive(false);
            talkButtons.SetActive(true);

            //Picks a random conversation
            conversation = conversations[Random.Range((int)0, (int)conversations.Count)];
            conversationIndex = 0;
            text.text = conversation.GetStatement(conversationIndex);
        }
    }

    public void NextTalk()
    {
        conversationIndex++;

        //Checks end of conversation hasnt been reached
        if(conversationIndex < conversation.GetCount())
        {
            text.text = conversation.GetStatement(conversationIndex);
        }
        else
        {
            MainMenu();
        }
    }

    //Run by Upgrade in game button
    public void UpgradesMenu()
    {

    }



}
