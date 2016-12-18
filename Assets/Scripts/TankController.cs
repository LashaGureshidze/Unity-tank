using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]
public class TankController : MonoBehaviour {
    public GameObject Tube;
    public GameObject Pivot;
    public GameObject TubeEnd;
    public Vector2 TubeDirection
    {
        get
        {
            Vector2 pivotPos = Camera.main.ScreenToWorldPoint(Pivot.transform.position);
            Vector2 tubeEndPos = Camera.main.ScreenToWorldPoint(TubeEnd.transform.position);

            return new Vector2(tubeEndPos.x - pivotPos.x, tubeEndPos.y - pivotPos.y);
        }
    }
    

    public Vector3 TubePosition
    {
        get
        {
            return TubeEnd.transform.position;
        }
    }

    private bool mouseDown;

    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            RemoveLine();
        }
        if (mouseDown)
        {
            DrawLine();
            MoveTube();
        }
    }


    private void MoveTube()
    {
        //mouse position from main camera
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pivotPosition = Camera.main.ScreenToWorldPoint(Pivot.transform.position);
        //mouse position from pivot.
        mousePosition = new Vector2(mousePosition.x - pivotPosition.x, mousePosition.y - pivotPosition.y);

        float AngleDeg = AngleBetween(TubeDirection, mousePosition);

        if (AngleDeg != 0)
        {
            Tube.transform.RotateAround(Pivot.transform.position, new Vector3(0, 0, 1), AngleDeg);
        }
    }

    private float AngleBetween(Vector2 vector1, Vector2 vector2)
    {
        float sin = vector1.x * vector2.y - vector2.x * vector1.y;
        float cos = vector1.x * vector2.x + vector1.y * vector2.y;

        return Mathf.Atan2(sin, cos) * (180 / Mathf.PI);
    }

    private void DrawLine()
    {
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(Pivot.transform.position));

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(mousePosition.x, mousePosition.y);
        lineRenderer.SetPosition(1, mousePosition);
    }

    private void RemoveLine()
    {
        lineRenderer.SetVertexCount(0);
    }
}
