using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField] private GameObject fishingTutorial;
    [SerializeField] private GameObject spearingTutorial;

    private bool fishTut = false;
    private bool spearTut = false;

    public void FishingTut()
    {
        if(!fishTut)
        {
            fishTut = true;
            fishingTutorial.SetActive(true);
        }
        else
        {
            fishingTutorial.SetActive(false);
        }
    }

    public void SpearingTut()
    {
        if (!spearTut)
        {
            spearTut = true;
            spearingTutorial.SetActive(true);
        }
        else
        {
            spearingTutorial.SetActive(false);
        }
    }

    public bool HasSpearTutHappened()
    {
        if(spearTut)
        {
            return true;
        }
        else
        {
            spearTut = true;
            return false;
        }
    }

}
