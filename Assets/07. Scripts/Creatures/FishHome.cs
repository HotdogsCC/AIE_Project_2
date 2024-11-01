using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHome : MonoBehaviour
{
    [SerializeField] public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawCube();
    }

    private void DrawCube()
    {
        DrawTopFace();
        DrawBottomFace();
        DrawEdges();
    }

    private void DrawTopFace()
    {
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z + radius), 
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z + radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z + radius),
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z - radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z - radius),
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z - radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z - radius),
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z + radius),
            Color.yellow);
    }

    private void DrawBottomFace()
    {
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z + radius),
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z + radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z + radius),
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z - radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z - radius),
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z - radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z - radius),
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z + radius),
            Color.yellow);
    }

    private void DrawEdges()
    {
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z + radius),
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z + radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z + radius),
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z + radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x - radius, transform.position.y + radius, transform.position.z - radius),
            new Vector3(transform.position.x - radius, transform.position.y - radius, transform.position.z - radius),
            Color.yellow);
        Debug.DrawLine(
            new Vector3(transform.position.x + radius, transform.position.y + radius, transform.position.z - radius),
            new Vector3(transform.position.x + radius, transform.position.y - radius, transform.position.z - radius),
            Color.yellow);
    }
}
