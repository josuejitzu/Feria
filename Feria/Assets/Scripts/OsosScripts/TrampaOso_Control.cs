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
    public GameObject spawnfx;
    public GameObject monedaPerdida;
    public GameObject monedaGanada;
    public bool dummy;

    public FMODUnity.StudioEventEmitter trampaClose_sfx,woosh_sfx;


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

    public IEnumerator ActivarTrampa()//Cuando spawnea
    {
        //activar humo aparicion
        print("Activando trampa: " + transform.name);
        trampa_anim.Play("trampa_idle", 0, 0.0f);

        spawnfx.GetComponent<ParticleSystem>().Play();
        monedaGanada.SetActive(false);
        monedaPerdida.SetActive(false);
        if (oso != null)
            oso = null;
        jaulaMesh.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        
        trampaMesh.SetActive(true);
        trigger.enabled = true;
        print("Trampa: " + transform.name +" activada");

    }

    public IEnumerator CapturarOso()//cuando captura a un osos
    {
        print("Trampa: " + transform.name + " accionada");
        trigger.enabled = false;

        StartCoroutine(oso.OsoCapturado());
        trampa_anim.SetTrigger("activar");
        woosh_sfx.Play();
        osoEnTrampa.SetActive(true);
        activacionfx.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.4f);
        humofx.GetComponent<ParticleSystem>().Play();
        monedaPerdida.SetActive(true);
        osoEnTrampa.SetActive(false);
        trampaMesh.SetActive(false);
        jaulaMesh.SetActive(true);
        trampaClose_sfx.Play();
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
        if (!dummy)
        {
            Osos_Master._masterOsos.SumarMoneda(5);
            Osos_Master._masterOsos.SumarTrampas();
        }
        
        monedaGanada.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        trampaMesh.SetActive(false);
        jaulaMesh.SetActive(false);
        print("Trampa: " + transform.name + "desactivada");
       
        if(!dummy)
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
        yield return new WaitForSeconds(0.6f);
        // humofx.SetActive(false);
        this.gameObject.SetActive(false);
        
    }

    public void ReproducirSFX(string p)
    {
        FMODUnity.RuntimeManager.PlayOneShot(p, this.transform.position);
    }
}
