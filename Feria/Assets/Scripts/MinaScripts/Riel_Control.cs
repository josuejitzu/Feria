using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riel_Control : MonoBehaviour
{

    // Use this for initialization
    public bool pasado;

    void Start()
    {

    }

    public void Activado()
    {
        StartCoroutine(Reseteo(10.0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            //Rieles_Control._rieles.ActivarRiel();
            StartCoroutine(Reseteo(5.0f));
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "carro")
        {
           // Rieles_Control._rieles.ActivarRiel();
            pasado = true;
          
        }

    }

    public IEnumerator Reseteo(float t)
    {

        yield return new WaitForSeconds(t);
        this.gameObject.SetActive(false);
        pasado = true;

    }

}
