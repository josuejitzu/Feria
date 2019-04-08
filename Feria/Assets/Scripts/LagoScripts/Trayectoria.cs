using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trayectoria : MonoBehaviour {


    Vector3 currentPos;
    public LineRenderer lineRenderer;
    public float velocidad, tiempo,distancia;
    public Transform objetivo;
    public float fuerzaCurva;
    public bool esCazador;
    // Use this for initialization.
    Vector3 dist;
	void Start () {
       // lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Vector3 dist = objetivo.transform.position - this.transform.position;
        if(esCazador)
        {
            dist = new Vector3(0.0f, 0.0f, fuerzaCurva * 0.4f);
        }
        else
         dist = new Vector3(0.0f, 0.0f, fuerzaCurva * -0.4f);//esta multiplicada por - para que la rotacion en z no gire con el arco y en el arco se pide la rotacion
        UpdateTrajectory(transform.localPosition, dist,  Vector3.down);

    }

    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 30; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetVertexCount(numSteps);
        lineRenderer.positionCount = numSteps;

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
