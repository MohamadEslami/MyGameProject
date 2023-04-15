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
        var Sc = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Sc.name);
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
