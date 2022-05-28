using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartHandler()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitHandler()
    {
        Application.Quit();
    }
}
