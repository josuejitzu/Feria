using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha_Control : MonoBehaviour
{
    public BoxCollider trigger;
    public BoxCollider colision;
    public Rigidbody rigid;
    public bool enArco;
    public GameObject arco_mesh;
    bool disparada;
    public Transform padre;
    public GameObject golpeCazador_FX;
    public GameObject trail;
    // Use this for initialization
    public FMODUnity.StudioEventEmitter swoosh_sfx;
	
	// Update is called once per frame
	void Update ()
    {
		if(disparada)
        {
            transform.LookAt(this.transform.position + rigid.velocity);
            swoosh_sfx.Play();
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "cazador")
        {
           StartCoroutine(other.GetComponent<Cazador_Control>().MatarCazador());
           StartCoroutine(DestruirCazador());
        }
        if(other.transform.tag == "arco" || other.transform.tag == "manoVR")
        {

        }else if(other.transform.tag =="pato")
        {
            other.GetComponent<Pato_Control>().MatarPato();
        }
        else
        {
            StartCoroutine(ReiniciarFlecha());
        }
        swoosh_sfx.Stop();
    }
    public void FlechaEnArco()
    {
        colision.enabled = false;
        trigger.enabled = false;
        rigid.useGravity = false;
        enArco = true;

    }
    public void FlechaDejoArco()
    {
        colision.enabled = true;
        trigger.enabled = true;
    }
    public void FlechaDisparada(float fuerza)
    {

        rigid.isKinematic = false;
        rigid.useGravity = true;
        rigid.velocity = this.transform.forward * fuerza;
        trail.SetActive(true);
        rigid.isKinematic = false;
        colision.enabled = true;
        trigger.enabled = true;
        disparada = true;
    }

    public IEnumerator DestruirCazador()
    {
        trail.SetActive(false);
        trigger.enabled = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
        //desparecerMesh
        arco_mesh.SetActive(false);
        //particula de golpe
        golpeCazador_FX.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ReiniciarFlecha());
    }
    public IEnumerator ReiniciarFlecha()
    {

     
        arco_mesh.SetActive(false);
        trigger.enabled = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
        trail.SetActive(false);
        disparada = false;
        yield return new WaitForSeconds(1.0f);
        golpeCazador_FX.SetActive(false);
        this.transform.position = padre.position;
        this.transform.rotation = padre.rotation;
        rigid.isKinematic = false;
        arco_mesh.SetActive(true);
        trigger.enabled = true;
        this.gameObject.SetActive(false);

    }
     
}
