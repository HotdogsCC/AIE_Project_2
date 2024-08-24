using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoss : MonoBehaviour
{
    [SerializeField] GameObject transitionGraphic;
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadGame(string sceneName)
    {
        Cursor.lockState = CursorLockMode.Locked;
        transitionGraphic.SetActive(true);
        StartCoroutine(LoadGamePt2(sceneName));
    }

    IEnumerator LoadGamePt2(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
