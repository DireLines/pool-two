using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ExitText : MonoBehaviour
{
    public string[] texts = { "Exiting", "Exiting.", "Exiting..", "Exiting..." };
    public float interval = 0.25f;
    int i = 0;
    float t;
    float holdTime;

    public TMP_Text textbox;

    private void Start()
    {
        i = 0;
        t = 0f;
        InvokeRepeating("SetText", 0f, 0.25f);
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void SetText()
    {
        textbox.text = texts[i++];
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            holdTime += Time.deltaTime;
            t += Time.deltaTime;
        }
        else
        {
            holdTime = 0f;
            t -= Time.deltaTime;
        }

        t = Mathf.Clamp01(t);
        textbox.color = new Color(1f, 1f, 1f, t);

        if (holdTime > 3f)
        {
            Application.Quit();
        }
    }
}
