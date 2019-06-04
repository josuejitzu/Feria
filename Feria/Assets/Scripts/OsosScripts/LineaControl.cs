using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LineaControl : MonoBehaviour
{
    public GameObject[] trampas;
    public bool jugando = true;
    public Transform objetivo;
    [Space(10)]
    [Header("Osos")]
    public GameObject osoPrefab;
    public List<GameObject> osos = new List<GameObject>();
    public int cantidadOsos;
    public bool conOso;
	// Use this for initialization

	void Start ()
    {
        DesactivarTrampas();
        for (int i = 0; i < 2; i++)
        {
            ActivarTrampas();
        }
        SpawnOsos();
	}

	public void DesactivarTrampas()
    {
        foreach(GameObject t in trampas)
        {
            t.SetActive(false);
        }
    }


	public void ActivarTrampas()
    {

        if (!jugando)
            return;

        if (conOso)
            return;

        int  rt = LoteriaTrampa();
        for (int i = 0; i < trampas.Length; i++)
        {
             if (i == rt)
             {
                    if (trampas[rt].activeInHierarchy)
                    {
                            rt = LoteriaTrampa();
                    }
                    
                    trampas[rt].SetActive(true);                
                    trampas[rt].GetComponent<TrampaOso_Control>().StopAllCoroutines();
                    StartCoroutine(trampas[rt].GetComponent<TrampaOso_Control>().ActivarTrampa());
             }

        }

    }

    public void SpawnOsos()
    {

        for (int i = 0; i < cantidadOsos; i++)
        {
            GameObject oso = Instantiate(osoPrefab, transform.position, Quaternion.identity);
            oso.SetActive(false);
            oso.transform.name = "oso" + i + " " + this.transform.name;
            osos.Add(oso);

        }

    }

    public void ActivarOso()
    {
        if (conOso)
        {
            OsosManada_Control._osos.ActivarOso();
            return; 

        }

        foreach (GameObject oso in osos)
        {
            if (!oso.activeInHierarchy)
            {


                oso.transform.position = this.transform.position;
                oso.transform.rotation = this.transform.rotation;
                oso.GetComponent<Oso_Control>().objetivo = objetivo;
                oso.GetComponent<Oso_Control>().lineaPadre = this;

                oso.SetActive(true);
                oso.GetComponent<Oso_Control>().gruñir_sfx.Play();
                conOso = true;
                break;
            }
        }
    }

    int LoteriaTrampa()
    {
        int r = Random.Range(0, trampas.Length);
        return r;
    }

    public void DesarmarTrampas()//Desarma las trampas activas al final del juego
    {
        foreach (GameObject t in trampas)
        {
           StartCoroutine( t.GetComponent<TrampaOso_Control>().DesactivarTrampa());
        }
    }

}
