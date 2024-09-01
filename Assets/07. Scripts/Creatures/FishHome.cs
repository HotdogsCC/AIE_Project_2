using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHome : MonoBehaviour
{
    [SerializeField] private float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + radius, transform.position.y, transform.position.z), Color.yellow);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x - radius, transform.position.y, transform.position.z), Color.yellow);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + radius, transform.position.z), Color.yellow);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - radius, transform.position.z), Color.yellow);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + radius), Color.yellow);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - radius), Color.yellow);
    }
}
