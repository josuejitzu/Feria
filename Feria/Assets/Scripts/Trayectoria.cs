using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trayectoria : MonoBehaviour {


    Vector3 currentPos;
    public LineRenderer lineRenderer;
    public float velocidad, tiempo,distancia;
    public Transform objetivo;
	// Use this for initialization
	void Start () {
        lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dist = objetivo.transform.position - this.transform.position;
        UpdateTrajectory(transform.position, dist,  Vector3.down);

    }

    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 30; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);

        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        for (int i = 0; i < numSteps; ++i)
        {
            lineRenderer.SetPosition(i, position);

            position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
            velocity += gravity * timeDelta;
        }
    }



}
