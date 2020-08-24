using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulsateText : MonoBehaviour
{
    public TMP_Text textbox;
    float t;

    // Update is called once per frame
    void Update()
    {
        textbox.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, Mathf.PingPong(t, 1f)));
        t += Time.deltaTime;
    }
}
