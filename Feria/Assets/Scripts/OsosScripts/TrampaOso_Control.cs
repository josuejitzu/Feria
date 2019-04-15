using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaOso_Control : MonoBehaviour
{
    public enum LineaTrampa {a,b,c,d,e,f,g}
    public LineaTrampa linea;
    // Use this for initialization
    public BoxCollider trigger;
    public Oso_Control oso;
    public GameObject trampaMesh;
    public Animator trampa_anim;
    public GameObject osoEnTrampa;
    public GameObject jaulaMesh;
    [Space(10)]
    [Header("FX")]
    public GameObject humofx;
    public GameObject activacionfx;
    public GameObject monedaPerdida;
    public GameObject monedaGanada;

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
        print("Activando trampa: " + transform.name);
        trampa_anim.Play("trampa_idle", 0, 0.0f);
        humofx.SetActive(false);
        monedaGanada.SetActive(false);
        monedaPerdida.SetActive(false);
        if (oso != null)
            oso = null;
        jaulaMesh.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        trampaMesh.SetActive(true);
        trigger.enabled = true;
        print("Trampa: " + transform.name +" activada");

    }
    public IEnumerator CapturarOso()
    {
        print("Trampa: " + transform.name + " accionada");
        trigger.enabled = false;

        StartCoroutine(oso.OsoCapturado());
        trampa_anim.SetTrigger("activar");
      
        osoEnTrampa.SetActive(true);
        activacionfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.4f);
        humofx.SetActive(true);
        monedaPerdida.SetActive(true);
        osoEnTrampa.SetActive(false);
        trampaMesh.SetActive(false);
        jaulaMesh.SetActive(true);
        Osos_Master._masterOsos.RestarMonedas(10);
        yield return new WaitForSeconds(1.5f);
        oso = null;
        StartCoroutine(ReiniciarTrampa());

    }
    public IEnumerator DesactivarTrampa()
    {
        activacionfx.GetComponent<ParticleSystem>().Play();
        trigger.enabled = false;
        trampa_anim.SetTrigger("desarmar");
        Osos_Master._masterOsos.SumarMoneda(5);
        Osos_Master._masterOsos.SumarTrampas();
        
        monedaGanada.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        trampaMesh.SetActive(false);
        jaulaMesh.SetActive(false);
        print("Trampa: " + transform.name + "desactivada");
       
        OsosManada_Control._osos.ActivarTrampa(linea.ToString());
        this.gameObject.SetActive(false);
    }
    public IEnumerator ReiniciarTrampa()
    {

        print("Trampa: " + transform.name + " reiniciando");
        trampaMesh.SetActive(false);
        jaulaMesh.SetActive(false);
        humofx.GetComponent<ParticleSystem>().Play();
        
        yield return new WaitForSeconds(1.0f);
        monedaGanada.SetActive(false);

        OsosManada_Control._osos.ActivarTrampa(linea.ToString());
        yield return new WaitForSeconds(0.5f);
        humofx.SetActive(false);
        this.gameObject.SetActive(false);
        
    }
}
