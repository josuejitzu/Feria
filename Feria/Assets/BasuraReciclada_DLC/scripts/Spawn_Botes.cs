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
    public float minRate, maxRate;
    float rate;
   
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
                rate = Time.time + maxRate;
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
                b.GetComponent<bote_control>().Activar(2.0f, objetivo,RandTipo(),_valor);
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

    bote_control.TipoBote RandTipo()
    {
        int r = Random.Range(0, 3);
        bote_control.TipoBote t = bote_control.TipoBote.organica;



        if(r == 0)
        {
            t = bote_control.TipoBote.organica;
        }else if(r == 1)
        {
            t = bote_control.TipoBote.inorganico;
        }else if(r == 2)
        {
            t = bote_control.TipoBote.reciclable;
        }

        return t;
    }
}
