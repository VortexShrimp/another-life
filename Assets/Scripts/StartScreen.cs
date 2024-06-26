using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadSceneAsync("Level One", LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
