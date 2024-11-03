using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] private float minTime = 10f;
    [SerializeField] private float maxTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop(Random.Range(minTime, maxTime)));
    }

    IEnumerator SpawnLoop(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(fish, transform.position, Quaternion.identity);
        StartCoroutine(SpawnLoop(Random.Range(minTime, maxTime)));
    }
}
