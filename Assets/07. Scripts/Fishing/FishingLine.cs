using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [SerializeField] private Transform plrBundle;

    private void Start()
    {
        transform.localPosition = new Vector3(-plrBundle.position.x, -plrBundle.position.y, -plrBundle.position.z);
    }
}
