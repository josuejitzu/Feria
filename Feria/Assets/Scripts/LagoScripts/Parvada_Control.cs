using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Parvada_Control : MonoBehaviour
{
    public static Parvada_Control _parvada;

    [Header("Tiempos")]
    public float rateSpawn;
    public float sigSpawn;
    [Space(10)]
    [Header("Puntos Spawnfin")]
    public Transform[] puntos;
    public Transform[] puntosFinal;
    public Transform[] puntosMalla;
    int puntoInicial;
    int puntoFinalPasado;
    int puntoMallaPasado;
    [Space(10)]
    [Header("Patos")]
    public GameObject patosA_prefab;
    public List<GameObject> patosA;
    public int cantidadPatos;
    public bool spawnear = true;
    GameObject objetivoPasado;
	// Use this for initialization
	void Start ()
    {
        _parvada = this;
        SpawnPatos();
        sigSpawn = rateSpawn;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Time.time > sigSpawn)
        {
            if (spawnear)
            {


                ActivarPatos();
                sigSpawn = Time.time + rateSpawn;
            }
        }
	}

    void SpawnPatos()
    {
        for (int i = 0; i < cantidadPatos; i++)
        {
            GameObject pato = Instantiate(patosA_prefab, transform.position, Quaternion.identity) as GameObject;
            pato.SetActive(false);
            pato.transform.name = "patoA_" + i;
            patosA.Add(pato);
        }

    }

    public void ActivarPatos()
    {
        foreach(GameObject p in patosA)
        {
            if(!p.activeInHierarchy)
            {
                Transform t = posInicial();
                p.transform.position = t.position;
                p.transform.rotation = t.rotation;
               
                for (int i = 0; i < 3; i++)
                {
                    if (i == 2)
                    {
                        p.GetComponent<Pato_Control>().camino[i] = posFinal();
                    }
                    else
                    {
                        p.GetComponent<Pato_Control>().camino[i] = posIntermedia();
                    }

                }
                p.SetActive(true);
                p.GetComponent<Pato_Control>().desplazarse = true;
              // Cazadores_Control._cazadores.ActivarCazadores();
                break;

            }

        }

    }

    Transform posInicial()
    {
        int rand = Random.Range(0, puntos.Length);
        Transform pos = puntos[rand];
        puntoInicial = rand;
        return pos;

    }
    Transform posFinal()
    {
        int rand = Random.Range(0, puntosFinal.Length);
        while(rand == puntoInicial && puntoFinalPasado == rand)
        {
            rand = Random.Range(0, puntosFinal.Length);
        }
        Transform pos = puntosFinal[rand];
        puntoFinalPasado = rand;


        return pos;
    }
    Transform posIntermedia()
    {
        int rand = Random.Range(0, puntosMalla.Length);
        while(rand == puntoMallaPasado)
        {
          rand = Random.Range(0, puntosMalla.Length);

        }
        Transform pos = puntosMalla[rand];
        puntoMallaPasado = rand;
        return pos;
    }

    public GameObject DarObjetivo()
    {
        GameObject objetivo = null;

        foreach(GameObject p in patosA)
        {
            if(p.activeInHierarchy)
            {
                if(p.GetComponent<Pato_Control>().enMira == false)
                {
                    if (p != objetivoPasado)
                    {
                        objetivo = p;
                        break;
                    }
                }
            }
        }
        objetivoPasado = objetivo;
        return objetivo;
    }
}
