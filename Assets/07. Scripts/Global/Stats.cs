using System.Collections;
using System.Collections.Generic;
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

    public static int GetBabarusaFishInv()
    {
        return babarusaFishInv;
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
