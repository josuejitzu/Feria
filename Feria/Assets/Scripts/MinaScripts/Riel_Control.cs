using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riel_Control : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            Rieles_Control._rieles.ActivarRiel();
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "carro")
        {
            Invoke("Reseteo", 5.0f);
        }

    }

    void Reseteo()
    {
        this.gameObject.SetActive(false);
    }

}
