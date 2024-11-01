using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    private static FishMovement[] fishies;

    public static void FreezeFishes()
    {
        fishies = FindObjectsOfType<FishMovement>();
        foreach (var fish in fishies)
        {
            fish.enabled = false;
        }
    }

    public static void FreezePlayer()
    {
        FirstPersonController player = FindObjectOfType<FirstPersonController>();
        player.enabled = false;
    }

    public static void FreezeBreathing()
    {
        Breathe breathe = FindObjectOfType<Breathe>();
        breathe.enabled = false;
    }

    public static void Freeze()
    {
        FreezeFishes();
        FreezePlayer();
        FreezeBreathing();
    }

    public static void ResumeFishes()
    {
        foreach (var fish in fishies)
        {
            if(fish != null)
            {
                Debug.Log("I am enabaling");
                fish.enabled = true;
            }
        }
    }

    public static void ResumePlayer()
    {
        FirstPersonController player = FindObjectOfType<FirstPersonController>();
        player.enabled = true;
    }

    public static void ResumeBreathing()
    {
        Breathe breathe = FindObjectOfType<Breathe>();
        breathe.enabled = true;
    }

    public static void Resume()
    {
        ResumePlayer();
        ResumeFishes();
        ResumeBreathing();
    }

}
