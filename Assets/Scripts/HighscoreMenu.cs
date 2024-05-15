using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreMenu : MonoBehaviour
{
    [SerializeField] private Text firstPlace;
    [SerializeField] private Text secondPlace;
    [SerializeField] private Text thirdPlace;
    private Text[] scores;
    private int firstScore;
    private int secondScore;
    private int thirdScore;
    private int playerScore;
    private int[] intScores;

    // Start is called before the first frame update
    void Start()
    {
        scores = new Text[] { firstPlace, secondPlace, thirdPlace };
        intScores = new int[] { firstScore, secondScore, thirdScore };
        UnityEngine.Debug.Log("staaaaaaart  ");
        UnityEngine.Debug.Log(firstScore);
        UnityEngine.Debug.Log(secondScore);
        UnityEngine.Debug.Log(thirdScore);
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(playerScore);

        for(int i = 0; i < 3; i++)
        {
            if(playerScore > intScores[i])
            {
                UnityEngine.Debug.Log("playerScore  " + playerScore);
                int temp = intScores[i];
                for (int j = i + 1; j < 3; j++)
                {
                    int temp1 = intScores[j];
                    intScores[j] = temp;
                    temp = temp1;
                }

                
                intScores[i] = playerScore;
                
                break;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            scores[j].text = j + 1 + ". " + intScores[j];
        }
        playerScore = 0;
    }
    
    void OnEnable()
    {
        //UnityEngine.Debug.Log("enable, firstScore:  " + 1 + ". " + intScores[0]);
        playerScore = PlayerPrefs.GetInt("currentScore");
        firstScore = PlayerPrefs.GetInt("firstScore");
        secondScore = PlayerPrefs.GetInt("secondScore");
        thirdScore = PlayerPrefs.GetInt("thirdScore");
        // firstPlace.text =  1 + ". " + intScores[0];
        firstPlace.gameObject.SetActive(true);
        secondPlace.gameObject.SetActive(true);
        thirdPlace.gameObject.SetActive(true);


    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", 0);
        UnityEngine.Debug.Log("disable, firstScore:  " + firstScore);
        PlayerPrefs.SetInt("firstScore", intScores[0]);
        PlayerPrefs.SetInt("secondScore", intScores[1]);
        PlayerPrefs.SetInt("thirdScore", intScores[2]);
        firstPlace.gameObject.SetActive(false);
        secondPlace.gameObject.SetActive(false);
        thirdPlace.gameObject.SetActive(false);
    }

}
