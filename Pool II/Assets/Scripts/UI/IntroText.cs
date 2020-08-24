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

    public GameObject playerButton;
    public GameObject musicSlider;
    public GameObject sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        title = text.text;
        characterCount = 0;
        StartCoroutine(Intro());

        playerButton.SetActive(false);
        musicSlider.SetActive(false);
        sfxSlider.SetActive(false);
    }

    IEnumerator Intro()
    {
        MusicManager.instance?.Play();
        while (characterCount <= title.Length)
        {
            text.text = title.Substring(0, characterCount);

            if (characterCount < title.Length)
            {
                if (title[characterCount] == ' ' || title[characterCount] == '\n')
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    yield return new WaitForSeconds(interval);
                }
            }

            characterCount++;
        }
        yield return new WaitForSeconds(interval);
        playerButton.SetActive(true);
        yield return new WaitForSeconds(interval);
        musicSlider.SetActive(true);
        yield return new WaitForSeconds(interval);
        sfxSlider.SetActive(true);
    }
}
