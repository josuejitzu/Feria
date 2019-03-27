using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soporte_Control : MonoBehaviour
{

    public GameObject trampa;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            StartCoroutine(Reiniciar());
        }

    }
    public IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }

    public void ActivarTrampa(int n)
    {
        if(n == 3)
        {
            trampa.SetActive(true);
        }
        else
        {
            return;
        }

    }
}
