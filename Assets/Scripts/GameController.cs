using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {
    public Text Score;

	// Use this for initialization
	void Start () {
        prepatePlayerPrefs();

        loadScore();
    }

    private void loadScore()
    {
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
        }

        int score = PlayerPrefs.GetInt("score");
        Score.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void prepatePlayerPrefs()
    {
        for (int i = 1; i <= 5; i++)
        {
            Debug.Log("putPrefs" + i);
            if (!PlayerPrefs.HasKey("levelDoneStatus" + i))
            {
                if (i == 1)
                {
                    PlayerPrefs.SetInt("levelDoneStatus" + i, 0);   //star count
                }
                else
                {
                    PlayerPrefs.SetInt("levelDoneStatus" + i, -1);
                }

            }
        }
    }
}
