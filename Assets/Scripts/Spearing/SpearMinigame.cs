using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearMinigame : MonoBehaviour
{
    [SerializeField] private GameObject cursor;

    private bool isGameRunning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cursor.transform.position = Input.mousePosition;
        Vector2 vectorFromOrigin = new Vector2(cursor.transform.localPosition.x, cursor.transform.localPosition.y);
        if (vectorFromOrigin.magnitude < 40)
        {
            Mouse.current.WarpCursorPosition(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        }
        
    }

}
