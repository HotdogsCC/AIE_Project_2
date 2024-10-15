using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoss : MonoBehaviour
{
    [SerializeField] GameObject transitionGraphic;
    [SerializeField] private TextMeshProUGUI text;
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            text.text = operation.progress.ToString();
            yield return null;
        }
    }
}
