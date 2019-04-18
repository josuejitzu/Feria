using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzadores_Control : MonoBehaviour
{
    public static Lanzadores_Control _lanzadores;
    public LanzadorBasura_Control[] lanzadores;
    int lanzadorPasado = 0;
    [Space(10)]
    [Header("Tiempo")]
    public float rateDisparo;
    float sigDisparo;
    public bool lanzar;
    public float rateA, rateB, rateC;
	// Use this for initialization
	void Start ()
    {
        _lanzadores = this;
        rateDisparo = rateA;
	}
	
	// Update is called once per frame
	void Update ()
    {

        TiempoLanzar();
	}
    void TiempoLanzar()
    {
        if (!lanzar)
            return;

        if (Time.time >= sigDisparo)
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
    public void CambiarVelocidad(int i)
    {
        if(i == 1)
        {
            rateDisparo = rateA;
        }else if(i == 2)
        {
            rateDisparo = rateB;
        }else if(i==3)
        {
            rateDisparo = rateC;
        }
    }
}
