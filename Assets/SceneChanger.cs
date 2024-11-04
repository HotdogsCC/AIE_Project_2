using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int sceneToGoTo;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            canvas.SetActive(true);
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
