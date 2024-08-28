using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class TextDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void DisplayText(string _text)
    {
        text.text = _text;
        Debug.Log("1 ran");
    }

    public void DisplayText(string _text, float time)
    {
        Debug.Log("2");
        DisplayText(_text);
        StartCoroutine(WaitForAMo(time));
    }

    private IEnumerator WaitForAMo(float time)
    {
        Debug.Log("3");
        yield return new WaitForSeconds(time);
        Debug.Log("4");
        DisplayText("");
    }
}
