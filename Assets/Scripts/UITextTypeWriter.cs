using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class UITextTypeWriter : MonoBehaviour


// attach to UI Text component (with the full text already there)
{
    [SerializeField] GameObject thisObject;
    [SerializeField] GameObject nextText;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject portal;
    [SerializeField] bool secondText;
    TextMeshProUGUI txt;
    string story;

    void Awake()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.06f);
        }
        if (secondText) nextText.SetActive(true);
        else
        {
            yield return new WaitForSeconds(2f);
            canvas.SetActive(false);
            portal.SetActive(true);
        }
        thisObject.SetActive(false);



    }

}
