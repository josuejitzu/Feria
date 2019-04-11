﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaControl : MonoBehaviour
{
    public GameObject[] trampas;

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
        int  rt = LoteriaTrampa();
        for (int i = 0; i < 6; i++)
        {
             if (i == rt)
             {
                    if (trampas[rt].activeInHierarchy)
                    {
                            rt = LoteriaTrampa();
                    }
                    trampas[rt].SetActive(true);
                    trampas[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
             }
        }

    }

    int LoteriaTrampa()
    {
        int r = Random.Range(0, trampas.Length);
        return r;
    }
}
