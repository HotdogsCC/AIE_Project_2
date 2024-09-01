using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatOceanNoise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes boat go up and down
        transform.position = new Vector3(transform.position.x,
            Mathf.Sin(Time.realtimeSinceStartup)/2 + 35.45f,
            transform.position.z);
        
        //makes boat rock side to side
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, Mathf.Sin(Time.realtimeSinceStartup/2) * 8);
    }
}
