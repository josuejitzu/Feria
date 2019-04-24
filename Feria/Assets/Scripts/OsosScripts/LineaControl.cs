using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaControl : MonoBehaviour
{
    public GameObject[] trampas;
    public bool jugando = true;
	// Use this for initialization
	void Start ()
    {
        DesactivarTrampas();
        for (int i = 0; i < 2; i++)
        {
            ActivarTrampas();
        }
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
