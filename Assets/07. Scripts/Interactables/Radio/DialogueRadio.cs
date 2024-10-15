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

        //sets text to upgrade cost
        text.text = @$"Speed: {swimUpgrades[speedLevel].GetSalmonCost()} salmon, {swimUpgrades[speedLevel].GetBabaroosaCost()} babaroosa
Breath: {breatheUpgrades[breatheLevel].GetSalmonCost()} salmon, {breatheUpgrades[breatheLevel].GetBabaroosaCost()} babaroosa
Health: {healthUpgrades[healthLevel].GetSalmonCost()} salmon, {healthUpgrades[healthLevel].GetBabaroosaCost()} babaroosa

You have: {Stats.GetNormalFishInv()} salmon, {Stats.GetBabarusaFishInv()} babaroosa";

        //sets interactivity of button based on money
        if(Stats.GetNormalFishInv() >= swimUpgrades[speedLevel].GetSalmonCost() && Stats.GetBabarusaFishInv() >= swimUpgrades[speedLevel].GetBabaroosaCost())
        {
            speedUpgradeButton.interactable = true;
        }
        else
        {
            speedUpgradeButton.interactable = false;
        }

        if (Stats.GetNormalFishInv() >= breatheUpgrades[breatheLevel].GetSalmonCost() && Stats.GetBabarusaFishInv() >= breatheUpgrades[breatheLevel].GetBabaroosaCost())
        {
            breatheUpgradeButton.interactable = true;
        }
        else
        {
            breatheUpgradeButton.interactable = false;
        }

        if (Stats.GetNormalFishInv() >= healthUpgrades[healthLevel].GetSalmonCost() && Stats.GetBabarusaFishInv() >= healthUpgrades[healthLevel].GetBabaroosaCost())
        {
            healthUpgradeButton.interactable = true;
        }
        else
        {
            healthUpgradeButton.interactable = false;
        }

    }

    public void SpeedUpgrade()
    {
        FindObjectOfType<GroundWaterManager>().waterMoveSpeed = swimUpgrades[speedLevel].GetResultOfUpgrade();
        Stats.RemoveNormalFishInv(swimUpgrades[speedLevel].GetSalmonCost());
        Stats.RemoveBabaroosaFishInv(swimUpgrades[speedLevel].GetBabaroosaCost());
        speedLevel++;
        MainMenu();
    }

    public void BreatheUpgrade()
    {
        FindObjectOfType<Breathe>().SetBreatheTime(breatheUpgrades[breatheLevel].GetResultOfUpgrade());
        Stats.RemoveNormalFishInv(breatheUpgrades[breatheLevel].GetSalmonCost());
        Stats.RemoveBabaroosaFishInv(breatheUpgrades[breatheLevel].GetBabaroosaCost());
        breatheLevel++;
        MainMenu();
    }

    public void HealthUpgrade()
    {
        FindObjectOfType<Health>().SetMaxHealth(healthUpgrades[healthLevel].GetResultOfUpgrade());
        Stats.RemoveNormalFishInv(healthUpgrades[healthLevel].GetSalmonCost());
        Stats.RemoveBabaroosaFishInv(healthUpgrades[healthLevel].GetBabaroosaCost());
        healthLevel++;
        MainMenu();
    }

}
