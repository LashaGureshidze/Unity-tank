using UnityEngine;
using System.Collections;
using System;

public class LevelController : MonoBehaviour {
    public GameObject Cube;
    public GameObject Num;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject Lock;
    public int level;

    public static int OneStarReq
    {
        get
        {
            return 10;
        }
    }
    public static int TwoStarReq
    {
        get
        {
            return 30;
        }
    }
    public static int ThreeStarReq
    {
        get
        {
            return 50;
        }
    }

    // Use this for initialization
    void Start () {
        visualize();
	}

    private void visualize()
    {
        Cube.SetActive(false);
        Num.SetActive(false);
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        Lock.SetActive(true);

        Debug.Log("visualize");
        if (PlayerPrefs.HasKey("levelDoneStatus" + level) && PlayerPrefs.GetInt("levelDoneStatus" + level) > -1)
        {
            
            int starCount = PlayerPrefs.GetInt("levelDoneStatus" + level);
            Debug.Log("level" + level + ", is opened. star count = " + starCount);

            Lock.SetActive(false);
            Cube.SetActive(true);
            Num.SetActive(true);
            if (starCount > 0)
            {
                Star1.SetActive(true);
            }
            if (starCount > 1)
            {
                Star2.SetActive(true);
            }
            if (starCount > 2)
            {
                Star3.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void onLevelClick()
    {
        Debug.Log("on enter level:" + level);

        if (!PlayerPrefs.HasKey("levelDoneStatus" + level) || PlayerPrefs.GetInt("levelDoneStatus" + level) == -1)
        {
            Debug.Log("WARN Level not yet opened");
            return;
        }

        Application.LoadLevel("Level" + level + "Scene");
    }
}
