using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public GameObject Tank;
    private TankController tankController;

    private GameObject ball;
    private BallController ballController;

    public int levelNum;

    public Text Score;
    public Text Level;
    public Text Bullets;

    private int bulletsCount = 5;
    public int BulletsCount
    {
        get
        {
            return bulletsCount;
        }
    }

    private bool mouseDown;

    // Use this for initialization
    void Start()
    {
        tankController = Tank.GetComponent<TankController>();
        //ballController = Ball.GetComponent<BallController>();

        reinstansateBall();

        loadPrefs();

        updateBullets();
        Level.text = "Level: " + levelNum;
        
    }

    private void reinstansateBall()
    {
        if (ball != null)
        {
            Destroy(ball);
            ball = null;
            ballController = null;
        }
        ball = Instantiate(Resources.Load("Prefabs/Ball")) as GameObject;
        ball.transform.SetParent(Tank.transform.parent.transform);
        ballController = ball.GetComponent<BallController>();
    }

    private void updateBullets()
    {
        Bullets.text = "Bullets: " + bulletsCount;
    }

    private void loadPrefs()
    {
        loadScore();
    }

    private void loadScore()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            Score.text = "Score: " + PlayerPrefs.GetInt("score");
        }
        else
        {
            Score.text = "Score: 0";
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletsCount == 0)
        {
            endGame(false);
            return;
        }
        ballController.Shoot(tankController.TubePosition, tankController.TubeDirection);

        bulletsCount--;
        updateBullets();
    }

    internal void turnFailed()
    {

        if (bulletsCount <= 0)
        {
            //TODO show dialog
            endGame(false);
        } else
        {
            startNewTurn();
        }
    }

    private void startNewTurn()
    {
        
        //TODO
        reinstansateBall();
    }

    internal void turnSuccess()
    {
        saveScore();

        endGame(true);
    }

    private void endGame(bool win)
    {
        showDialog(win);

        destroyObjects();

        saveState(win);
    }

    private void saveState(bool win)
    {
        int starC = 0;
        if (bulletsCount == 4) starC = 3;
        if (bulletsCount == 3) starC = 2;
        if (bulletsCount == 2) starC = 1;
        PlayerPrefs.SetInt("levelDoneStatus" + levelNum, starC);

        //open next level
        if (win)
        {
            if (levelNum <= 4)
            {
                if (!PlayerPrefs.HasKey("levelDoneStatus" + (levelNum + 1)) || PlayerPrefs.GetInt("levelDoneStatus" + (levelNum + 1)) == -1)
                {
                    PlayerPrefs.SetInt("levelDoneStatus" + (levelNum + 1), 0);
                }
            }
        }
    }

    private void destroyObjects()
    {
        //destry objects
        if (ball != null)
        {
            Destroy(ball);
            ball = null;
        }
        if (Tank != null)
        {
            Destroy(Tank);
            Tank = null;
        }
    }

    private void showDialog(bool win)
    {
        GameObject dialog = Instantiate(Resources.Load("Prefabs/Dialog")) as GameObject;
        dialog.transform.SetParent(Tank.transform.parent.transform);
        dialog.transform.position = Tank.transform.parent.transform.position;
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.setParameters(win, levelNum);
    }

    private void saveScore()
    {
        int curScore = (bulletsCount + 1) * 10;
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", curScore);
        }
        else
        {
            PlayerPrefs.SetInt("score", curScore + PlayerPrefs.GetInt("score"));
        }
    }
}
