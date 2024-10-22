using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static int[] fishCaught = { 0, 0, 0, 0, 0, 0, 0, 0 };
    private static int[] fishInv = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public static int GetFishCaught(int index)
    {
        return fishCaught[index];
    }

    public static int GetFishInv(int index)
    {
        return fishInv[index];
    }

    public static void RemoveFishInv(int index, int amount)
    {
        Debug.Log("removed " + amount);
        fishInv[index] -= amount;
        if (fishInv[index] < 0)
        {
            Debug.Log("HEY! fish has gone below zero. Please check Stats.cs");
        }
    }

    public static void FishCaught(int fishIndex)
    {
        fishCaught[fishIndex]++;
        fishInv[fishIndex]++;

        switch (fishIndex)
        {
            case 0:
                Debug.Log("normal fish caught");
                break;
            case 1:
                Debug.Log("babarusa fish caught");
                break;
            case 2:
                Debug.Log("face fish caught");
                break;
            case 3:
                Debug.Log("coral fish caught");
                break;
            case 4:
                Debug.Log("box fish caught");
                break;
            case 5:
                Debug.Log("lamprey fish caught");
                break;
            case 6:
                Debug.Log("eyeless fish caught");
                break;
            case 7:
                Debug.Log("lure jaw fish caught");
                break;
            default:
                Debug.Log("Unknown fish index type");
                break;
        }
    }
}
