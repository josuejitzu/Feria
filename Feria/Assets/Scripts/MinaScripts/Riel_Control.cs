using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Riel_Control : MonoBehaviour
{

    // Use this for initialization
    public bool pasado;
    public Trampa_Control trampa;
    public GameObject[] murcielagos;
    public GameObject[] monedas;

    public Animator anim;
    public void Activado()
    {
        //StartCoroutine(Reseteo(10.0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            //Rieles_Control._rieles.ActivarRiel();
            //StartCoroutine(Reseteo(10.0f));
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "carro")
        {
            
            StartCoroutine(Reseteo(7.0f));
          
          
        }

    }

    public IEnumerator Reseteo(float t)
    {

        yield return new WaitForSeconds(t);
        foreach(GameObject m in murcielagos)
        {
            m.SetActive(false);
        }
        foreach(GameObject moneda in monedas)
        {
            moneda.SetActive(false);
        }

        this.gameObject.SetActive(false);
      

    }
    public void SetTrampa(int t)
    {
        if(t == 0) { return; }

        trampa.gameObject.SetActive(true);
        if(t == 1)
        {
            trampa._tipoTrampa = Trampa_Control.TipoTrampa.barrera;
            trampa.CambiarTrampa();

        }else if(t == 2)
        {
            trampa._tipoTrampa = Trampa_Control.TipoTrampa.piedras;
            trampa.CambiarTrampa();
        }
        else
        {
            trampa.gameObject.SetActive(false);
           // print("Trampa no activada");
        }


    }
    public void SetMonedas(int cantidad)//max 3
    {
        if (cantidad == 0)
            return;

        for (int i = 0; i < cantidad; i++)
        {
            monedas[i].SetActive(true);
            Rieles_Control._rieles.cantidadMonedas++;
        }
        
    }
    public void SetMurcielagos(int n)//max 4 //Llamado desde Rieles_Control
    {

        /*
        for (int i = 0; i < n; i++)
        {
            murcielagos[i].SetActive(true);
        }
        */
        if (n == 0)
        {
            murcielagos[0].SetActive(true);
            murcielagos[0].GetComponentInChildren<Animator>().SetFloat("velocidadAnim", Random.Range(1.0f, 1.8f));
        }

    }

}
