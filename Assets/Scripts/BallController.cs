using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{

    private WorldController worldController;

    public float speed;

    private Rigidbody2D body;
    public int HitLeft
    {
        get
        {
            return hitLeft;
        }
    }
    private int hitLeft;


    // Use this for initialization
    void Start()
    {
        hitLeft = 5;
        transform.gameObject.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        worldController = Camera.main.GetComponent<WorldController>();
    }

    public void Shoot(Vector3 position, Vector2 direction)
    {
        transform.position = position;
        transform.gameObject.SetActive(true);

        body.velocity = direction * speed;
    }

    void update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log("hit" + collisionInfo.gameObject.ToString());
        if (collisionInfo.gameObject.ToString().Contains("Enemy"))
        {
            winGame();
            return;
        }

        hitLeft--;
        if (hitLeft < 0)
        {
            endGame();
            return;
        }
        
    }

    private void winGame()
    {
        worldController.turnSuccess();
    }

    private void endGame()
    {
        worldController.turnFailed();
    }
}
