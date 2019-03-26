using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasMinas_Control : MonoBehaviour
{

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
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }


}
