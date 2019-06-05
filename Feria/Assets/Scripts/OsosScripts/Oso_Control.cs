using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Oso_Control : MonoBehaviour
{
    public Transform objetivo;
    public LineaControl lineaPadre;
    public float velocidadFinal;
    public float velocidadMax, velocidadMin;
    public NavMeshAgent agente;
    public Animator oso_anim;
    // public FMODUnity.StudioEventEmitter gruñir_sfx;
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
        oso_anim.gameObject.SetActive(true);
        lineaPadre.conOso = false;
        lineaPadre = null;
        this.gameObject.SetActive(false);


    }
    public IEnumerator OsoCapturado()
    {
        lineaPadre.conOso = false;
        velocidadFinal = 0.0f;
        agente.isStopped = true;
        oso_anim.gameObject.SetActive(false);
        Osos_Master._masterOsos.RestarOso();
        yield return new WaitForSeconds(0.5f);
        
        yield return new WaitForSeconds(2.0f);
        this.transform.parent = null;
        StartCoroutine(ReiniciarOso());
       
    }

    public void PlayPisada()
    {
       // pisada_sfx.Play();
    }
}
