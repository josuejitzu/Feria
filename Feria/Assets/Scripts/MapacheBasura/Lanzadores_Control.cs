using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzadores_Control : MonoBehaviour
{

    public LanzadorBasura_Control[] lanzadores;
    int lanzadorPasado = 0;
    [Space(10)]
    [Header("Tiempo")]
    public float rateDisparo;
    float sigDisparo;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

		if(Time.time >= sigDisparo)
        {
            DispararMapache();
            sigDisparo = Time.time + rateDisparo;     
        }

	}

    public void DispararMapache()
    {
        StartCoroutine(lanzadores[LanzadorRand()].MapacheLanzando());
    }

    int LanzadorRand()
    {
        int r = Random.Range(0, lanzadores.Length);
        while(r == lanzadorPasado)
        {
            r = Random.Range(0, lanzadores.Length);
        }
        lanzadorPasado = r;
        return r;
    }
}
