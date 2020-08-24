using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{

    public void PlayAgainYay()
    {
        SceneLoader.LoadNextScene(SceneTransition.CircleWipe);
    }
}
