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
    }

    public void DisplayText(string _text, float time)
    {
        DisplayText(_text);
        StartCoroutine(WaitForAMo(time));
    }

    private IEnumerator WaitForAMo(float time)
    {
        yield return new WaitForSeconds(time);
        DisplayText("");
    }
}
