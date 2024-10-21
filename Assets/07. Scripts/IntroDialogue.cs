using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] GameObject items;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] List<string> messages;

    private int messageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        text.text = messages[messageIndex];
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
    }

    public void Next()
    {
        messageIndex++;
        if(messages.Count > messageIndex)
        {
            text.text = messages[messageIndex];
        }
        else
        {
            StartGame();
        }
    }

    void StartGame()
    {
        //GameObject.FindGameObjectWithTag("mainCinemachine").GetComponent<CinemachineVirtualCamera>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        StartCoroutine(WaitAMo());
    }

    IEnumerator WaitAMo()
    {
        yield return new WaitForSeconds(1f);
        FindFirstObjectByType<FirstPersonController>().enabled = true;
        items.SetActive(true);
        mainCanvas.SetActive(true);
        Destroy(gameObject);
    }
}
