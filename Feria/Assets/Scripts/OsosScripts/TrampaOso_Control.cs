using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaOso_Control : MonoBehaviour
{
    public enum LineaTrampa { a,b,c,d,e,f,g}
    public LineaTrampa linea;
    // Use this for initialization
    public BoxCollider trigger;
    public Oso_Control oso;
    public GameObject trampaMesh;
    public GameObject jaulaMesh;
    [Space(10)]
    [Header("FX")]
    public GameObject humofx;
    public GameObject monedaPerdida;

	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "oso")
        {
            oso = other.GetComponent<Oso_Control>();
            StartCoroutine(CapturarOso());
        }

    }
    public IEnumerator ActivarTrampa()
    {
        //activar humo aparicion
        oso = null;
        jaulaMesh.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        trampaMesh.SetActive(true);
        trigger.enabled = true;
    }
    public IEnumerator CapturarOso()
    {
        trigger.enabled = false;
        oso.gameObject.transform.parent = this.transform;
        StartCoroutine(oso.OsoCapturado());
        //activar humo fx
        //activar animacion de oso caputurado
        trampaMesh.SetActive(false);
        //aparecer jaula
        
        yield return new WaitForSeconds(3.0f);
        //activar humo fx
        yield return new WaitForSeconds(0.3f);
       // StartCoroutine(oso.ReiniciarOso());
        StartCoroutine(ReiniciarTrampa());

    }
    public IEnumerator ReiniciarTrampa()
    {
        //desactivar humo1fx
        //desactivar humo2fx

        yield return new WaitForSeconds(1.0f);
        // trigger.enabled = true;
        
        this.gameObject.SetActive(false);
        OsosManada_Control._osos.ActivarTrampa(linea.ToString());
    }
}
