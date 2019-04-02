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
    // Use this for initialization
 
	
	// Update is called once per frame
	void Update ()
    {
		if(disparada)
        {
            transform.LookAt(this.transform.position + rigid.velocity);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "cazador"|| other.transform.tag == "arco" || other.transform.tag == "manoVR")
        {

        }
        else
        {
            StartCoroutine(ReiniciarFlecha());
        }
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
        
        rigid.isKinematic = false;
        colision.enabled = true;
        trigger.enabled = true;
        disparada = true;
    }
    public IEnumerator ReiniciarFlecha()
    {

     
        arco_mesh.SetActive(false);
        trigger.enabled = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
        disparada = false;
        yield return new WaitForSeconds(1.0f);
        this.transform.position = padre.position;
        this.transform.rotation = padre.rotation;
        rigid.isKinematic = false;
        arco_mesh.SetActive(true);
        trigger.enabled = true;
        this.gameObject.SetActive(false);

    }
     
}
