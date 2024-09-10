using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static int normalFishCaught = 0;
    private static int babarusaFishCaught = 0;

    public static int GetNormalFishCaught()
    {
        return normalFishCaught;
    }

    public static int GetBabarusaFishCaught()
    {
        return babarusaFishCaught;
    }

    public static void FishCaught(int fishIndex)
    {
        switch (fishIndex)
        {
            case 0:
                normalFishCaught++;
                Debug.Log("normal fish caught");
                break;
            case 1:
                babarusaFishCaught++;
                Debug.Log("babarusa fish caught");
                break;
            default:
                Debug.Log("Unknown fish index type");
                break;
        }
    }
}
