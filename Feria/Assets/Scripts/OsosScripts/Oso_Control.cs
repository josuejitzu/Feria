﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Oso_Control : MonoBehaviour
{
    public Transform objetivo;

    public float velocidadFinal;
    public float velocidadMax, velocidadMin;
    public NavMeshAgent agente;
    public Animator oso_anim;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        agente.speed = velocidadFinal;
        agente.destination = objetivo.position;
        Vector3 dist = objetivo.position - this.transform.position;
        if(dist.magnitude < 0.5f)
        {
            StartCoroutine(ReiniciarOso());
        }
	}
    public IEnumerator ReiniciarOso()
    {
        velocidadFinal = 0.55f;
        
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);


    }
    public IEnumerator OsoCapturado()
    {
        velocidadFinal = 0.0f;
        agente.isStopped = true;
        oso_anim.SetTrigger("brinco");
        yield return new WaitForSeconds(0.5f);
        oso_anim.SetTrigger("capturado");
        yield return new WaitForSeconds(2.9f);
        this.transform.parent = null;
        StartCoroutine(ReiniciarOso());
       
    }
}
