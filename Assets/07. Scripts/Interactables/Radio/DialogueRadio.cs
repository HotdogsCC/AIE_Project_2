using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRadio : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private List<UpgradeRequirements> breatheUpgrades;
    [SerializeField] private List<UpgradeRequirements> healthUpgrades;
    [SerializeField] private List<UpgradeRequirements> swimUpgrades;

    [Header("Dialogue")]
    [SerializeField] private List<string> introMessages;
    [SerializeField] private List<string> upgradeMessages;
    [SerializeField] private List<ConvoSO> conversations;

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject upgradeButtons;
    [SerializeField] private GameObject talkButtons;

    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button breatheUpgradeButton;
    [SerializeField] private Button healthUpgradeButton;

    private byte speedLevel = 0;
    private byte breatheLevel = 0;
    private byte healthLevel = 0;
    

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

    public void MainMenu()
    {

        //Shows the correct buttons
        mainMenuButtons.SetActive(true);
        talkButtons.SetActive(false);
        upgradeButtons.SetActive(false);

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
        upgradeButtons.SetActive(true);
        mainMenuButtons.SetActive(false);

        //doesn't display the cost if it is 0
        text.text = "Speed: ";
        if (swimUpgrades[speedLevel].GetCost(0) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(0)} salmon, ";
        }
        if (swimUpgrades[speedLevel].GetCost(1) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(1)} babaroosa, ";
        }
        if (swimUpgrades[speedLevel].GetCost(2) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(2)} face fish, ";
        }
        if (swimUpgrades[speedLevel].GetCost(3) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(3)} coral fish, ";
        }
        if (swimUpgrades[speedLevel].GetCost(4) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(4)} box fish, ";
        }
        if (swimUpgrades[speedLevel].GetCost(5) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(2)} lampery, ";
        }
        if (swimUpgrades[speedLevel].GetCost(6) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(6)} eyeless fish, ";
        }
        if (swimUpgrades[speedLevel].GetCost(7) != 0)
        {
            text.text += $"{swimUpgrades[speedLevel].GetCost(7)} lure jaw, ";
        }

        text.text += "\nBreath: ";
        if (breatheUpgrades[breatheLevel].GetCost(0) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(0)} salmon, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(1) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(1)} babaroosa, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(2) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(2)} face fish, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(3) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(3)} coral fish, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(4) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(4)} box fish, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(5) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(2)} lampery, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(6) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(6)} eyeless fish, ";
        }
        if (breatheUpgrades[breatheLevel].GetCost(7) != 0)
        {
            text.text += $"{breatheUpgrades[breatheLevel].GetCost(7)} lure jaw, ";
        }

        text.text += "\nHealth: ";
        if (healthUpgrades[healthLevel].GetCost(0) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(0)} salmon, ";
        }
        if (healthUpgrades[healthLevel].GetCost(1) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(1)} babaroosa, ";
        }
        if (healthUpgrades[healthLevel].GetCost(2) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(2)} face fish, ";
        }
        if (healthUpgrades[healthLevel].GetCost(3) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(3)} coral fish, ";
        }
        if (healthUpgrades[healthLevel].GetCost(4) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(4)} box fish, ";
        }
        if (healthUpgrades[healthLevel].GetCost(5) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(2)} lampery, ";
        }
        if (healthUpgrades[healthLevel].GetCost(6) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(6)} eyeless fish, ";
        }
        if (healthUpgrades[healthLevel].GetCost(7) != 0)
        {
            text.text += $"{healthUpgrades[healthLevel].GetCost(7)} lure jaw, ";
        }

        text.text += $"\n\nYou have: {Stats.GetFishInv(0)} salmon, {Stats.GetFishInv(1)} babaroosa, {Stats.GetFishInv(2)} face fish, {Stats.GetFishInv(3)} coral fish, {Stats.GetFishInv(4)} box fish, {Stats.GetFishInv(5)} lampery, {Stats.GetFishInv(6)} eyeless fish, {Stats.GetFishInv(7)} lure jaw";

        //sets interactivity of button based on money
        speedUpgradeButton.interactable = true;
        for (int i = 0; i < 8; i++)
        { 
            //if you dont have enough money, break
            if(Stats.GetFishInv(i) < swimUpgrades[speedLevel].GetCost(i))
            {
                speedUpgradeButton.interactable = false;
                break;
            }
        }

        breatheUpgradeButton.interactable = true;
        for (int i = 0; i < 8; i++)
        {
            //if you dont have enough money, break
            if (Stats.GetFishInv(i) < breatheUpgrades[breatheLevel].GetCost(i))
            {
                breatheUpgradeButton.interactable = false;
                break;
            }
        }

        healthUpgradeButton.interactable = true;
        for (int i = 0; i < 8; i++)
        {
            //if you dont have enough money, break
            if (Stats.GetFishInv(i) < healthUpgrades[healthLevel].GetCost(i))
            {
                healthUpgradeButton.interactable = false;
                break;
            }
        }

    }

    public void SpeedUpgrade()
    {
        FindObjectOfType<GroundWaterManager>().waterMoveSpeed = swimUpgrades[speedLevel].GetResultOfUpgrade();
        Stats.RemoveFishInv(0, swimUpgrades[speedLevel].GetCost(0));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(1));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(2));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(3));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(4));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(5));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(6));
        Stats.RemoveFishInv(1, swimUpgrades[speedLevel].GetCost(7));
        speedLevel++;
        MainMenu();
    }

    public void BreatheUpgrade()
    {
        FindObjectOfType<Breathe>().SetBreatheTime(breatheUpgrades[breatheLevel].GetResultOfUpgrade());
        Stats.RemoveFishInv(0, breatheUpgrades[breatheLevel].GetCost(0));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(1));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(2));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(3));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(4));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(5));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(6));
        Stats.RemoveFishInv(1, breatheUpgrades[breatheLevel].GetCost(7));
        breatheLevel++;
        MainMenu();
    }

    public void HealthUpgrade()
    {
        FindObjectOfType<Health>().SetMaxHealth(healthUpgrades[healthLevel].GetResultOfUpgrade());
        Stats.RemoveFishInv(0, healthUpgrades[healthLevel].GetCost(0));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(1));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(2));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(3));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(4));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(5));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(6));
        Stats.RemoveFishInv(1, healthUpgrades[healthLevel].GetCost(7));
        healthLevel++;
        MainMenu();
    }

}
