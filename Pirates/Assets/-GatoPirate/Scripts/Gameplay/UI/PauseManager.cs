using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
