using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class TerrainFriendPrefabGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;

    private void Start()
    {
        foreach (GameObject rock in prefabs)
        {
            Transform curRock = Instantiate(rock).transform;
            Transform childRock = curRock.GetChild(0).GetChild(0);
            childRock.transform.SetParent(null);
            childRock.name = curRock.name;
            Destroy(curRock.gameObject);
        }
    }
}
