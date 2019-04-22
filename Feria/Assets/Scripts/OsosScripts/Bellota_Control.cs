using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellota_Control : MonoBehaviour
{
    public SphereCollider trigger;
    public CapsuleCollider collision;
    public Rigidbody rigid;
    // Use this for initialization
    public GameObject bellota_mesh;
    public bool enMano;

	void Start ()
    {
      
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag =="manoVR")
        {
            print(this.transform.name + " en mano");
        }
        if(other.transform.tag == "trampaOso")
        {
            StartCoroutine(other.GetComponent<TrampaOso_Control>().DesactivarTrampa());
            StartCoroutine(Reiniciar());
        }
        if(other.transform.tag == "terreno")
        {
            Invoke("GolpeNormal",2.0f);
        }
    }

    public void BellotaAgarrada()
    {
        //trigger.enabled = false;
        collision.enabled = false;
        rigid.isKinematic = true;
    }

    public void BellotaSoltada()
    {
        rigid.isKinematic = false;
        collision.enabled = true;
        trigger.enabled = true;
    }
    public void GolpeNormal()
    {
        StartCoroutine(Reiniciar());
    }
    public IEnumerator Reiniciar()
    {
        trigger.enabled = false;
        bellota_mesh.SetActive(false);
        MunicionBellota_Control._bellotas.cantidadBellotas -= 1;
        yield return new WaitForSeconds(1.0f);

        Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

}
