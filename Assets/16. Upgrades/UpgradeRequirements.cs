using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeRequirements : ScriptableObject
{
    [Header("Cost of Upgrade")]
    [SerializeField] private byte salmon;
    [SerializeField] private byte babaroosa;
    [SerializeField] private byte faceFish;
    [SerializeField] private byte coralFish;
    [SerializeField] private byte boxFish;
    [SerializeField] private byte lampery;
    [SerializeField] private byte eyeless;
    [SerializeField] private byte lureJaw;

    [Header("Result of Upgrade")]
    [SerializeField] private float value;



    public byte GetCost(int index)
    {
        switch (index)
        {
            case 0:
                return salmon;
            case 1:
                return babaroosa;
            case 2:
                return faceFish;
            case 3:
                return coralFish;
            case 4:
                return boxFish;
            case 5:
                return lampery;
            case 6:
                return eyeless;
            case 7:
                return lureJaw;
            default:
                return 0;
        }
    }

    public float GetResultOfUpgrade()
    {
        return value;
    }
}
