using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void OnEnable()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("currentScore"));
        PlayerPrefs.SetInt("currentScore", 0);


    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
