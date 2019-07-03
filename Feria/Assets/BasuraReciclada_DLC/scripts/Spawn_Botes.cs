using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawn_Botes : MonoBehaviour
{
    public GameObject bote_prefab;
    public List<GameObject> botes = new List<GameObject>();
    public int cantidad;
    public Transform objetivo;
    public bool spawnear;
    public float minRate= 1f, maxRate;
    public float minVel = 1.0f, maxVel;
    float rate;
    int anterior,repetida = 0;
    public bote_control.Valor _valor;
    // Use this for initialization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, objetivo.position);
    }

    void Start ()
    {
        SpawnearBotes();
        rate = Time.time + maxRate;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnear)
        {
            if(Time.time >= rate)
            {
                ActivarBote();
                rate = Time.time + RandomRate();
            }
        }
	}
    public void ActivarBote()
    {
        foreach(GameObject b in botes)
        {
            if(b.activeInHierarchy == false)
            {
                b.SetActive(true);
                b.GetComponent<bote_control>().Activar(RandVelocidad(), objetivo,RandTipo(),_valor);
                break;
            }
        }
    }
    public void SpawnearBotes()
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject bote = Instantiate(bote_prefab, this.transform.position, this.transform.rotation) as GameObject;
            bote.transform.name = "bote_"+i +"_"+ this.transform.name;
            bote.GetComponent<bote_control>().padre = this.gameObject;
            bote.SetActive(false);
            botes.Add(bote);
        }
    }

    basura_botes.TipoBasura RandTipo()
    {
        int r = Random.Range(0, 3);
        basura_botes.TipoBasura t = basura_botes.TipoBasura.organica;

        if(r == anterior)
        {
            repetida++;
            if(repetida > 1)
            {
                while(r == anterior)
                {
                    r = Random.Range(0, 3);
                }
                repetida = 0;
            }
        }

        if(r == 0)
        {
            t = basura_botes.TipoBasura.organica;
        }
        else if(r == 1)
        {
            t = basura_botes.TipoBasura.inorganica;
        }
        else if(r == 2)
        {
            t = basura_botes.TipoBasura.reciclable;
        }
        anterior = r;

        return t;
    }

    float RandomRate()
    {
        float r = Random.Range(minRate, maxRate);
        return r;
    }

    float RandVelocidad()
    {
        float r = Random.Range(minVel, maxVel);
        return r;
    }

    public void FinJuego()
    {
        foreach(GameObject bote in botes)
        {
            if(bote.activeInHierarchy)
            {
                bote.GetComponent<bote_control>().velocidad = 10.0f;
            }
        }
    }
}
