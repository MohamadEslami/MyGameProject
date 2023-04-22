using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void BackHomeBtn()
    {
        SceneManager.LoadScene("Menu");
    }
    public void AgainBtn()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1; // Set the time scale back to 1
    }


    public void ChangeSceneBtn(string Name)
    {
        SceneManager.LoadScene(Name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
