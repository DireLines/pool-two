using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IntroText : MonoBehaviour
{
    [Range(0, 1)]
    public float interval;
    TMP_Text text;
    string title;
    int characterCount;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        title = text.text;
        characterCount = 0;
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        MusicManager.instance.Play();
        while (characterCount < title.Length)
        {
            text.text = title.Substring(0, characterCount);

            if (title[characterCount] == ' ' || title[characterCount] == '\n')
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }

            characterCount++;
        }
    }
}
