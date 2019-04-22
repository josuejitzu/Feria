using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
public class Pato_Control : MonoBehaviour
{
    public float velocidadFinal;
    public float velMax,velMin;
    public float velocidad;
    public float velocidadRotacion;
    public Transform[] camino = new Transform[3];
    public Transform finRuta;
    public bool desplazarse;
    public NavMeshAgent agente;
    public bool enMira;
    public int enPos;
    public Animator pato_anim;
    public GameObject monedasPerdidas_fx;
    public GameObject pato_mesh;
    public GameObject particulaExplosion;
    bool rotar;

	// Use this for initialization
	void Start ()
    {
        enPos = 0;
        agente = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (desplazarse)
        {

            if (agente.speed <= 0)
                agente.speed = velocidad;

            agente.speed = velocidad;
            agente.destination = camino[enPos].position;
            Vector3 dist = camino[enPos].position - this.transform.position;

            if (dist.magnitude <= 0.4f)
            {
                //enPos++;
                StartCoroutine(CambioDireccion());
                if (enPos == 3)
                {
                    desplazarse = false;
                    StartCoroutine(Reiniciar());
                }

            }

            
        }
        if(rotar)
        {
            Vector3 distRot = camino[enPos].position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(distRot), Time.deltaTime * velocidadRotacion);
        }
      

	}
    IEnumerator CambioDireccion()
    {
        desplazarse = false;
        agente.speed = 0.1f;
       
        enPos++;
        yield return new WaitForSeconds(0.3f);
        rotar = true;
        yield return new WaitForSeconds(1.0f);
        agente.speed = velocidad;
        desplazarse = true;
        rotar = false;
    }
    public IEnumerator MatarPato()
    {
       
        velocidad = 0;
        pato_anim.SetTrigger("muerte");
        desplazarse = false;
        agente.speed = 0;
        particulaExplosion.SetActive(true);
        Master_Patos._masterPatos.ScorePatos();
        Master_Patos._masterPatos.RestarMonedas(5);
        monedasPerdidas_fx.SetActive(true);
        enMira = false;
        yield return new WaitForSeconds(0.7f);
        pato_mesh.SetActive(false);
       // print("Termino secuencia muerte...");
        yield return new WaitForSeconds(0.5f);
        //print("Llamando Reinicio");
        StartCoroutine(Reiniciar());
    }
    public IEnumerator Reiniciar()
    {
       // print("Reiniciando...");
        enPos = 0;
        monedasPerdidas_fx.SetActive(false);
        enMira = false;
        yield return new WaitForSeconds(0.1f);
      
        velocidad = 1.0f;
        enMira = false;
        particulaExplosion.SetActive(false);
     
        pato_mesh.SetActive(true);
        this.gameObject.SetActive(false);

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "posFinal")
        {
            desplazarse = false;
            StartCoroutine(Reiniciar());
        }
    }
}
