using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static int normalFishCaught = 0;
    private static int babarusaFishCaught = 0;

    private static int normalFishInv = 0;
    private static int babarusaFishInv = 0;

    public static int GetNormalFishCaught()
    {
        return normalFishCaught;
    }

    public static int GetBabarusaFishCaught()
    {
        return babarusaFishCaught;
    }

    public static int GetNormalFishInv()
    {
        return normalFishInv;
    }

    public static void RemoveNormalFishInv(int amount)
    {
        Debug.Log("removed " + amount);
        normalFishInv -= amount;
        if(normalFishInv < 0)
        {
            Debug.Log("HEY! Normal fish has gone below zero. Please check Stats.cs");
        }
    }

    public static int GetBabarusaFishInv()
    {
        return babarusaFishInv;
    }

    public static void RemoveBabaroosaFishInv(int amount)
    {
        babarusaFishInv -= amount;
        if (babarusaFishInv < 0)
        {
            Debug.Log("HEY! babaroosa fish has gone below zero. Please check Stats.cs");
        }
    }

    public static void FishCaught(int fishIndex)
    {
        switch (fishIndex)
        {
            case 0:
                normalFishCaught++;
                normalFishInv++;
                Debug.Log("normal fish caught");
                break;
            case 1:
                babarusaFishCaught++;
                babarusaFishInv++;
                Debug.Log("babarusa fish caught");
                break;
            default:
                Debug.Log("Unknown fish index type");
                break;
        }
    }
}
