using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour {
    public GameObject SuccessText;
    public GameObject FailedText;

    private int level;
    private bool win;



	// Use this for initialization
	void Start () {
        SuccessText.SetActive(false);
        FailedText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (win)
        {
            SuccessText.SetActive(true);
        }
        else
        {
            FailedText.SetActive(true);
        }
    }

    public void setParameters(bool win, int level)
    {
        this.level = level;
        this.win = win;
    }

    public void onTryAgainClick()
    {
        Application.LoadLevel("Level" + level + "Scene");
    }

    public void onSchooseLevelClick()
    {
        Application.LoadLevel("Level");
    }
}
