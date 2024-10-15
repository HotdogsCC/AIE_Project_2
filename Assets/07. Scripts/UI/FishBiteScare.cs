using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBiteScare : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject biteSFX;
    float t = 1;

    private void Update()
    {
        t += Time.deltaTime;
        image.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
    }

    public void Scare()
    {
        t = 0;
        Instantiate(biteSFX);
    }
}
