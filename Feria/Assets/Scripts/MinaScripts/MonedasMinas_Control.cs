using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasMinas_Control : MonoBehaviour
{
    public GameObject moneda_mesh;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            Master_Minas._mina.SumarMonedas();
            StartCoroutine(Reinicio());
        }

    }

    public IEnumerator Reinicio()
    {
        //moneda_mesh.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //moneda_mesh.SetActive(true);
        this.gameObject.SetActive(false);
    }


}
