using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassSpinner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.timeSinceLevelLoad) * 30f);
    }
}
