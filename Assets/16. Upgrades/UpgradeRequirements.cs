using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeRequirements : ScriptableObject
{
    [Header("Cost of Upgrade")]
    [SerializeField] private byte salmon;
    [SerializeField] private byte babaroosa;

    [Header("Result of Upgrade")]
    [SerializeField] private float value;

    public byte GetSalmonCost()
    {
        return salmon;
    }

    public byte GetBabaroosaCost()
    {
        return babaroosa;
    }

    public float GetResultOfUpgrade()
    {
        return value;
    }
}
